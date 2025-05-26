using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.HospitalSchedules;
using MyApp.Application.Validators.HospitalSchedules;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Shared.Services;

namespace MyApp.Application.UseCases.HospitalSchedules
{
    public class HospitalScheduleUpdateUseCase
    {
        private readonly IGenericRepository<HospitalSchedulesEntity> _hospitalScheduleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<HospitalScheduleUpdateUseCase> _logger;

        public HospitalScheduleUpdateUseCase(
            IGenericRepository<HospitalSchedulesEntity> hospitalScheduleRepository,
            IMapper mapper,
            ILogger<HospitalScheduleUpdateUseCase> logger)
        {
            _logger = logger;
            _hospitalScheduleRepository = hospitalScheduleRepository;
            _mapper = mapper;
        }

        public async Task<HospitalScheduleResponse> Execute(int HospitalScheduleId, HospitalScheduleRequest request)
        {
            _logger.LogInformation("Iniciando la busqueda del horario del hospital con el HospitalScheduleId: {HospitalScheduleId}", HospitalScheduleId);

            var validator = new HospitalScheduleValidator();
            ValidatorHelper.ValidateAndThrow(request, validator);

            HospitalSchedulesEntity? searchEntity = await _hospitalScheduleRepository.GetByCondition(x => x.HospitalScheduleId == HospitalScheduleId);

            if (searchEntity is null)
            {
                _logger.LogWarning("No se encontró el horario del hospital solicitado con el HospitalScheduleId: {HospitalScheduleId}", HospitalScheduleId);
                throw new NotFoundException("No se encontró el registro solicitado.");
            }

            var entityToUpdate = _mapper.Map<HospitalSchedulesEntity>(request);

            var updatedEntity = await _hospitalScheduleRepository.Update(searchEntity, entityToUpdate);

            var response = _mapper.Map<HospitalScheduleResponse>(updatedEntity);

            _logger.LogInformation("Se actualizo correctamente el horario del hospital con el HospitalScheduleId: {HospitalScheduleId}", response.HospitalScheduleId);

            return response;
        }
    }
}
