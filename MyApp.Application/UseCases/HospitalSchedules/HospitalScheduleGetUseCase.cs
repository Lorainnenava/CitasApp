using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.HospitalSchedules;
using MyApp.Application.Interfaces.UseCases.HospitalSchedules;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.HospitalSchedules
{
    public class HospitalScheduleGetUseCase : IHospitalScheduleGetUseCase
    {
        private readonly IGenericRepository<HospitalSchedulesEntity> _hospitalScheduleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<HospitalScheduleGetUseCase> _logger;

        public HospitalScheduleGetUseCase(
            IGenericRepository<HospitalSchedulesEntity> hospitalScheduleRepository,
            IMapper mapper,
            ILogger<HospitalScheduleGetUseCase> logger)
        {
            _logger = logger;
            _hospitalScheduleRepository = hospitalScheduleRepository;
            _mapper = mapper;
        }

        public async Task<HospitalScheduleResponse> Execute()
        {
            _logger.LogInformation("Iniciando la busqueda del horario del hospital.");

            IEnumerable<HospitalSchedulesEntity> searchEntity = await _hospitalScheduleRepository.GetAll(null, x => x.ScheduleDetails);

            if (!searchEntity.Any())
            {
                _logger.LogWarning("El hospital aun no tiene un horario registrado.");
                throw new NotFoundException("El hospital no tiene horario.");
            }

            var response = _mapper.Map<HospitalScheduleResponse>(searchEntity.FirstOrDefault());

            _logger.LogInformation("Horario del hospital encontrado exitosamente.");

            return response;
        }
    }
}
