using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Doctors;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Doctors
{
    public class DoctorCreateUseCase
    {
        private readonly IGenericRepository<DoctorsEntity> _doctorRepository;
        private readonly IGenericRepository<HospitalSchedulesEntity> _hospitalScheduleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorCreateUseCase> _logger;

        public DoctorCreateUseCase(
            IGenericRepository<DoctorsEntity> doctorRepository,
            IGenericRepository<HospitalSchedulesEntity> hospitalScheduleRepository,
            IMapper mapper,
            ILogger<DoctorCreateUseCase> logger)
        {
            _logger = logger;
            _doctorRepository = doctorRepository;
            _hospitalScheduleRepository = hospitalScheduleRepository;
            _mapper = mapper;
        }

        public async Task<DoctorResponse> Execute(DoctorRequest request)
        {
            _logger.LogInformation("Iniciando la creación de un doctor con n° de licencia: {License}", request.LicenseNumber);

            var emailExisted = await _doctorRepository.GetByCondition(x => x.LicenseNumber == request.LicenseNumber);

            if (emailExisted != null)
            {
                _logger.LogWarning("Iniciando la creación de un doctor con n° de licencia: {License}", request.LicenseNumber);
                throw new AlreadyExistsException($"El doctor con n° de licencia {request.LicenseNumber} ya está registrado.");
            }

            var hospitalSchedules = await _hospitalScheduleRepository.GetAll();

            if (!hospitalSchedules.Any())
            {
                throw new NotFoundException("No hay horarios de hospital configurados.");
            }

            foreach (var schedule in request.DoctorSchedules)
            {
                var hospitalSchedule = hospitalSchedules
                    .FirstOrDefault(x => schedule.StartTime < x.StartTime || schedule.EndTime > x.EndTime);

                if (hospitalSchedule is null)
                {
                    throw new InvalidDataException("El horario del doctor no está dentro del rango de horario del hospital.");
                }

                // TODO: se debe validar que el dia este en dentro de los permitidos en el hospital
                if (schedule.AllDays && schedule.DayOfWeek != null)
                {
                    throw new InvalidDataException($"El día {schedule.DayOfWeek} no está permitido por el hospital.");
                }
            }

            var entityMapped = _mapper.Map<DoctorsEntity>(request);

            var doctorCreated = await _doctorRepository.Create(entityMapped);

            var response = _mapper.Map<DoctorResponse>(doctorCreated);

            _logger.LogInformation("Doctor creado exitosamente con DoctorId: {UserId}", doctorCreated.DoctorId);

            return response;
        }
    }
}
