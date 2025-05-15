using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Appointments;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.DTOs;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Appointments
{
    public class AppointmentGetAllPaginatedUseCase
    {
        private readonly IGenericRepository<HospitalSchedulesEntity> _hospitalScheduleRepository;
        private readonly IGenericRepository<DoctorSchedulesEntity> _doctorScheduleRepository;
        private readonly ILogger<AppointmentGetAllPaginatedUseCase> _logger;

        public AppointmentGetAllPaginatedUseCase(
            IGenericRepository<HospitalSchedulesEntity> hospitalScheduleRepository,
            IGenericRepository<DoctorSchedulesEntity> doctorScheduleRepository,
            ILogger<AppointmentGetAllPaginatedUseCase> logger)
        {
            _logger = logger;
            _hospitalScheduleRepository = hospitalScheduleRepository;
            _doctorScheduleRepository = doctorScheduleRepository;
        }

        public async Task<PaginationResult<AppointmentResponse>> Execute(AppointmentGetAllPaginatedRequest request, int page, int size)
        {
            _logger.LogInformation("Iniciando la obtención de todas las citas paginadas.");

            var searchHospitalSchedule = await _hospitalScheduleRepository.GetAll();

            int interval = searchHospitalSchedule.First().AppointmentDurationMinutes >= 0
                ? searchHospitalSchedule.First().AppointmentDurationMinutes
                : 20;

            var doctorSchedules = await _doctorScheduleRepository.GetAll(x => x.DoctorId == request.DoctorId, x => x.Doctor);

            var doctor = doctorSchedules.First().Doctor;

            if (doctorSchedules.Count() == 0)
            {
                _logger.LogWarning("No se encontró ningún doctor con el ID: {DoctorId}", request.DoctorId);
                throw new NotFoundException("No se encontro el doctor solicitado");
            }

            if (doctor.IsActive == false || doctor.IsOnVacation == true)
            {
                _logger.LogWarning("El doctor con el ID: {DoctorId} se encuentra inactivo o esta de vacaciones", request.DoctorId);
                throw new InvalidOperationException("El doctor solicitado esta inactivo o se encuentra de vacaciones");
            }

            var availableSlots = new List<AppointmentResponse>();

            DateOnly startDate = request.AppointmentDate;
            DateOnly endDate = startDate.AddDays(7);

            DateOnly? startVacation = doctor.StartDateVacation != null
                ? DateOnly.FromDateTime(doctor.StartDateVacation.Value)
                : null;

            DateOnly? endVacation = doctor.EndDateVacation != null
                ? DateOnly.FromDateTime(doctor.EndDateVacation.Value)
                : null;

            if (startDate >= startVacation && startDate <= endVacation)
            {
                throw new ConflictException("El doctor está de vacaciones en la fecha seleccionada.");
            }

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (startVacation.HasValue && endVacation.HasValue)
                {
                    if (date >= startVacation.Value && date <= endVacation.Value)
                    {
                        break;
                    }
                }

                var dayOfWeek = date.DayOfWeek;

                var doctorScheduleForDay = doctorSchedules.FirstOrDefault(ds => ds.DayOfWeek == dayOfWeek);

                if (doctorScheduleForDay is null)
                {
                    continue;
                }

                var startTime = doctorScheduleForDay.StartTime;
                var endTime = doctorScheduleForDay.EndTime;

                for (var time = startTime; time.AddMinutes(interval) <= endTime; time = time.AddMinutes(interval))
                {
                    availableSlots.Add(new AppointmentResponse
                    {
                        AppointmentDate = date,
                        AppointmentTime = time,
                        DoctorId = request.DoctorId
                    });
                }
            }

            var totalCount = availableSlots.Count;
            var pagedSlots = availableSlots
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();

            _logger.LogInformation("Se obtuvieron {Count} citas.", totalCount);

            return new PaginationResult<AppointmentResponse>
            {
                RowsCount = totalCount,
                PageCount = (int)Math.Ceiling((double)totalCount / size),
                PageSize = size,
                CurrentPage = page,
                Results = pagedSlots
            };
        }

    }
}
