using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Hospitals;
using MyApp.Application.Interfaces.UseCases.Hospitals;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Hospitals
{
    public class HospitalToogleIsActiveUseCase : IHospitalToogleIsActiveUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<HospitalToogleIsActiveUseCase> _logger;
        private readonly IGenericRepository<HospitalsEntity> _hospitalRepository;

        public HospitalToogleIsActiveUseCase(
            IMapper mapper,
            ILogger<HospitalToogleIsActiveUseCase> logger,
            IGenericRepository<HospitalsEntity> hospitalEntity)
        {
            _logger = logger;
            _mapper = mapper;
            _hospitalRepository = hospitalEntity;
        }

        public async Task<HospitalResponse> Execute(int HospitalId)
        {
            _logger.LogInformation("Iniciando la actualización del estado del hospital con ID: {HospitalId}", HospitalId);

            var existingHospital = await _hospitalRepository.GetByCondition(x => x.HospitalId == HospitalId);

            if (existingHospital is null)
            {
                _logger.LogWarning("No se encontró un hospital con el ID: {HospitalId}", HospitalId);
                throw new NotFoundException("No se encontró un hospital registrado con ese identificador.");
            }

            existingHospital.IsActive = !existingHospital.IsActive;
            existingHospital.UpdatedAt = DateTime.UtcNow;

            var updatedHospital = await _hospitalRepository.Update(existingHospital);

            _logger.LogInformation("Estado del hospital actualizado correctamente. ID: {HospitalId}, Estado actual: {Estado}",
                updatedHospital.HospitalId, updatedHospital.IsActive ? "Activo" : "Inactivo");

            var response = _mapper.Map<HospitalResponse>(updatedHospital);

            return response;
        }
    }
}
