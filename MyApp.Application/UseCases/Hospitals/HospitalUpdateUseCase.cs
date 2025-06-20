using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Hospitals;
using MyApp.Application.Interfaces.UseCases.Hospitals;
using MyApp.Application.Validators.Hospitals;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Shared.Services;

namespace MyApp.Application.UseCases.Hospitals
{
    public class HospitalUpdateUseCase : IHospitalUpdateUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<HospitalUpdateUseCase> _logger;
        private readonly IGenericRepository<HospitalsEntity> _hospitalRepository;

        public HospitalUpdateUseCase(
            IMapper mapper,
            ILogger<HospitalUpdateUseCase> logger,
            IGenericRepository<HospitalsEntity> hospitalEntity)
        {
            _logger = logger;
            _mapper = mapper;
            _hospitalRepository = hospitalEntity;
        }

        public async Task<HospitalResponse> Execute(int HospitalId, HospitalUpdateRequest request)
        {
            _logger.LogInformation("Iniciando actualización de hospital con ID: {HospitalId}", HospitalId);

            var validator = new HospitalUpdateValidator();

            ValidatorHelper.ValidateAndThrow(request, validator);

            var existingHospital = await _hospitalRepository.GetByCondition(x => x.HospitalId == HospitalId);

            if (existingHospital is null)
            {
                _logger.LogWarning("No se encontró un hospital con ID: {HospitalId}", HospitalId);
                throw new NotFoundException("No se encontró un hospital registrado con ese identificador.");
            }

            var mapped = _mapper.Map<HospitalsEntity>(request);

            mapped.UpdatedAt = DateTime.UtcNow;

            var updatedHospital = await _hospitalRepository.Update(existingHospital, mapped);

            _logger.LogInformation("Hospital actualizado exitosamente con ID: {HospitalId}", updatedHospital.HospitalId);

            var response = _mapper.Map<HospitalResponse>(updatedHospital);

            return response;
        }
    }
}
