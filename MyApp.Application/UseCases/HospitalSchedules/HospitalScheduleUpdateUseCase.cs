using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.HospitalSchedules;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.HospitalSchedules
{
    public class HospitalScheduleUpdateUseCase
    {
        private readonly IGenericRepository<HospitalSchedulesEntity> _hospitalScheduleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<HospitalScheduleCreateUseCase> _logger;

        public HospitalScheduleUpdateUseCase(
            IGenericRepository<HospitalSchedulesEntity> hospitalScheduleRepository,
            IMapper mapper,
            ILogger<HospitalScheduleCreateUseCase> logger)
        {
            _logger = logger;
            _hospitalScheduleRepository = hospitalScheduleRepository;
            _mapper = mapper;
        }

        public async Task<HospitalScheduleResponse> Execute(int HospitalScheduleId, HospitalScheduleRequest request)
        {
            _logger.LogInformation("Iniciando la busqueda del horario del hospital con el ID: {HospitalScheduleId}", HospitalScheduleId);

            HospitalSchedulesEntity? searchEntity = await _hospitalScheduleRepository.GetByCondition(x => x.HospitalScheduleId == HospitalScheduleId);

            if (searchEntity is null)
            {
                _logger.LogWarning("No se encontró el horario del hospital solicitado con el ID: {HospitalScheduleId}", HospitalScheduleId);
                throw new NotFoundException("No se encontró el registro solicitado.");
            }

            var response = _mapper.Map<HospitalScheduleResponse>(searchEntity);

            _logger.LogInformation("Horario del hospital encontrado exitosamente con el ID: {HospitalScheduleId}", response.HospitalScheduleId);

            return response;
        }
    }
}
