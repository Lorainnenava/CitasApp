using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Appointments;
using MyApp.Application.Interfaces.UseCases.Appointments;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Appointments
{
    public class AppointmentCreateUseCase : IAppointmentCreateUseCase
    {
        private readonly IGenericRepository<HospitalSchedulesEntity> _hospitalScheduleRepository;
        private readonly IGenericRepository<AppointmentsEntity> _appointmentRepository;
        private readonly IGenericRepository<DoctorsEntity> _doctorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentCreateUseCase> _logger;

        public AppointmentCreateUseCase(
            IGenericRepository<HospitalSchedulesEntity> hospitalScheduleRepository,
            IGenericRepository<AppointmentsEntity> appointmentRepository,
            IGenericRepository<DoctorsEntity> doctorRepository,
            IMapper mapper,
            ILogger<AppointmentCreateUseCase> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _hospitalScheduleRepository = hospitalScheduleRepository;
        }

        public async Task<AppointmentResponse> Execute(AppointmentRequest request)
        {
            _logger.LogInformation("Iniciando la creación de una cita medica");

            var hospitalSchedule = await _hospitalScheduleRepository.GetAll();

            if (!hospitalSchedule.Any())
            {
                throw new ConflictException("No puedes solicitar una cita ya que no hay horarios configurados para el hospital.");
            }

            var scheduleValid = hospitalSchedule.Any(schedule =>
            {
                var dayAllowed = schedule.AllDays || request.AppointmentDate.DayOfWeek switch
                {
                    DayOfWeek.Monday => schedule.Monday,
                    DayOfWeek.Tuesday => schedule.Tuesday,
                    DayOfWeek.Wednesday => schedule.Wednesday,
                    DayOfWeek.Thursday => schedule.Thursday,
                    DayOfWeek.Friday => schedule.Friday,
                    DayOfWeek.Saturday => schedule.Saturday,
                    _ => false
                };

                var timeAllowed = request.AppointmentTime >= schedule.StartTime
                               && request.AppointmentTime < schedule.EndTime;


                return dayAllowed && timeAllowed;
            });

            if (!scheduleValid)
            {
                throw new ConflictException("No se puede agendar una cita en ese día u horario. El hospital no atiende en ese momento.");
            }

            var doctor = await _doctorRepository.GetByCondition(
                d => d.DoctorId == request.DoctorId,
                x => x.DoctorSchedules
            );

            if (doctor is null)
            {
                throw new NotFoundException($"No se encontró al doctor solicitado.");
            }

            var isAvailable = doctor.DoctorSchedules.Any(s =>
                (s.AllDays.HasValue && s.AllDays.Value || s.DayOfWeek == request.AppointmentDate.DayOfWeek) &&
                request.AppointmentTime >= s.StartTime &&
                request.AppointmentTime <= s.EndTime
            );

            if (!isAvailable)
            {
                throw new ConflictException("La hora seleccionada no está dentro del horario disponible del doctor.");
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var now = TimeOnly.FromDateTime(DateTime.UtcNow);

            if (request.AppointmentDate < today ||
                (request.AppointmentDate == today && request.AppointmentTime <= now))
            {
                throw new ConflictException("No se puede agendar una cita en una fecha u hora pasada.");
            }

            var appointmentExisted = await _appointmentRepository.GetByCondition(
                x => x.DoctorId == request.DoctorId &&
                x.AppointmentDate == request.AppointmentDate &&
                x.AppointmentTime == request.AppointmentTime);

            if (appointmentExisted is not null)
            {
                throw new ConflictException("No es posible agendar esta cita. Ya existe una cita registrada con este doctor en la misma fecha y hora.");
            }

            var doctorOnVacation = await _doctorRepository.GetByCondition(
                x => x.DoctorId == request.DoctorId &&
                     x.StartDateVacation.HasValue &&
                     x.EndDateVacation.HasValue &&
                     request.AppointmentDate >= DateOnly.FromDateTime(x.StartDateVacation.Value) &&
                     request.AppointmentDate <= DateOnly.FromDateTime(x.EndDateVacation.Value)
            );

            if (doctorOnVacation is not null)
            {
                throw new ConflictException("El doctor no está disponible en la fecha seleccionada debido a vacaciones.");
            }

            var entityMapped = _mapper.Map<AppointmentsEntity>(request);

            var createEntity = await _appointmentRepository.Create(entityMapped);

            var response = _mapper.Map<AppointmentResponse>(createEntity);

            return response;
        }
    }
}
