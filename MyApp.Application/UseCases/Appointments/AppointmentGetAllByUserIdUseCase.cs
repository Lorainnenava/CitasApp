using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Appointments;
using MyApp.Application.Enums;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.DTOs;

namespace MyApp.Application.UseCases.Appointments
{
    public class AppointmentGetAllByUserIdUseCase
    {
        private readonly IGenericRepository<AppointmentsEntity> _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentGetAllByUserIdUseCase> _logger;

        public AppointmentGetAllByUserIdUseCase(
            IGenericRepository<AppointmentsEntity> appointmentRepository,
            IMapper mapper,
            ILogger<AppointmentGetAllByUserIdUseCase> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<PaginationResult<AppointmentResponse>> Execute(int UserId, int StatusId, int page = 1, int size = 10)
        {
            _logger.LogInformation("Iniciando la obtención de todas las citas del usuario con el ID: {ID}.", UserId);

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var appointments = await _appointmentRepository.GetAll(x =>
                x.UserId == UserId &&
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

            var (items, totalCount) = await _appointmentRepository.Pagination(page, size, x => x.UserId == UserId && x.StatusId == StatusId);

            var mappedItems = _mapper.Map<IEnumerable<AppointmentResponse>>(items);

            _logger.LogInformation("Se obtuvieron {Count} citas del usuario con el ID: {ID}.", UserId, totalCount);

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
