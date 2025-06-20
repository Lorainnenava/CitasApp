using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Hospitals;
using MyApp.Application.Interfaces.UseCases.Hospitals;
using MyApp.Application.Validators.Hospitals;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Services;

namespace MyApp.Application.UseCases.Hospitals
{
    public class HospitalCreateUseCase : IHospitalCreateUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<HospitalCreateUseCase> _logger;
        private readonly IGenericRepository<HospitalsEntity> _hospitalRepository;

        public HospitalCreateUseCase(
            IMapper mapper,
            ILogger<HospitalCreateUseCase> logger,
            IGenericRepository<HospitalsEntity> hospitalEntity)
        {
            _logger = logger;
            _mapper = mapper;
            _hospitalRepository = hospitalEntity;
        }

        public async Task<HospitalResponse> Execute(HospitalCreateRequest request)
        {
            _logger.LogInformation("Iniciando creación de hospital con nombre: {HospitalName}", request.Name);

            var validator = new HospitalCreateValidator();
            ValidatorHelper.ValidateAndThrow(request, validator);

            var existingHospital = await _hospitalRepository.GetByCondition(x => x.Nit == request.Nit);

            if (existingHospital is not null)
            {
                _logger.LogWarning("Ya existe un hospital con el nit: {Nit}", request.Nit);
                throw new InvalidOperationException("Ya existe un hospital registrado con este NIT.");
            }

            var mapped = _mapper.Map<HospitalsEntity>(request);

            var createdHospital = await _hospitalRepository.Create(mapped);

            _logger.LogInformation("Hospital creado exitosamente con ID: {HospitalId}", createdHospital.HospitalId);

            var response = _mapper.Map<HospitalResponse>(createdHospital);

            return response;
        }
    }
}
