using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Appointments;
using MyApp.Application.Enums;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.DTOs;

namespace MyApp.Application.UseCases.Appointments
{
    public class AppointmentGetAllByDoctorIdUseCase
    {
        private readonly IGenericRepository<AppointmentsEntity> _appointmentRepository;
        private readonly ILogger<AppointmentGetAllByDoctorIdUseCase> _logger;
        private readonly IMapper _mapper;

        public AppointmentGetAllByDoctorIdUseCase(
            IGenericRepository<AppointmentsEntity> appointmentRepository,
            IMapper mapper,
            ILogger<AppointmentGetAllByDoctorIdUseCase> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<PaginationResult<AppointmentResponse>> Execute(int DoctorId, int StatusId, int page = 1, int size = 10)
        {
            _logger.LogInformation("Iniciando la obtención de todas las citas asignadas al doctor con el ID: {DoctorId}.", DoctorId);

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var appointments = await _appointmentRepository.GetAll(x =>
                x.AppointmentDate < today &&
                x.StatusId != (int)StatusIdEnum.Confirmada);

            if (appointments is not null)
            {
                foreach (var item in appointments)
                {
                    item.StatusId = 12;
                    await _appointmentRepository.Update(x => x.AppointmentId == item.AppointmentId, item, item);
                }
            }

            var (items, totalCount) = await _appointmentRepository.Pagination(page, size, x => x.DoctorId == DoctorId && x.StatusId == StatusId);

            var mappedItems = _mapper.Map<IEnumerable<AppointmentResponse>>(items);

            _logger.LogInformation("Se obtuvieron {Count} citas asignadas al doctor con el ID: {ID}.", DoctorId, totalCount);

            return new PaginationResult<AppointmentResponse>
            {
                RowsCount = totalCount,
                PageCount = (int)Math.Ceiling((double)totalCount / size),
                PageSize = size,
                CurrentPage = page,
                Results = mappedItems
            };
        }
    }
}
