using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.HospitalSchedules;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;

namespace MyApp.Application.UseCases.HospitalSchedules
{
    public class HospitalScheduleCreateUseCase
    {
        private readonly IGenericRepository<HospitalSchedulesEntity> _hospitalScheduleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<HospitalScheduleCreateUseCase> _logger;

        public HospitalScheduleCreateUseCase(
            IGenericRepository<HospitalSchedulesEntity> hospitalScheduleRepository,
            IMapper mapper,
            ILogger<HospitalScheduleCreateUseCase> logger)
        {
            _logger = logger;
            _hospitalScheduleRepository = hospitalScheduleRepository;
            _mapper = mapper;
        }

        public async Task<HospitalScheduleResponse> Execute(HospitalScheduleRequest request)
        {
            _logger.LogInformation("Iniciando la creación del horario del hospital");

            var entityMapped = _mapper.Map<HospitalSchedulesEntity>(request);

            var entityCreated = await _hospitalScheduleRepository.Create(entityMapped);

            var response = _mapper.Map<HospitalScheduleResponse>(entityCreated);

            _logger.LogInformation("Horario del hospital creado exitosamente con el ID: {HospitalScheduleId}", response.HospitalScheduleId);

            return response;
        }
    }
}
