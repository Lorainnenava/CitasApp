using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Hospitals;
using MyApp.Application.Interfaces.UseCases.Hospitals;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Hospitals
{
    public class HospitalGetByIdUseCase : IHospitalGetByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<HospitalGetByIdUseCase> _logger;
        private readonly IGenericRepository<HospitalsEntity> _hospitalRequestRepository;

        public HospitalGetByIdUseCase(
            IMapper mapper,
            ILogger<HospitalGetByIdUseCase> logger,
            IGenericRepository<HospitalsEntity> changeHospitalRequestRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _hospitalRequestRepository = changeHospitalRequestRepository;
        }

        public async Task<HospitalResponse> Execute(int HospitalId)
        {
            _logger.LogInformation("Buscando el hospital con ID: {HospitalId}", HospitalId);

            var hospitalExisted = await _hospitalRequestRepository.GetByCondition(x => x.HospitalId == HospitalId);

            if (hospitalExisted is null)
            {
                _logger.LogWarning("No se encontró el hospital con ID: {HospitalId}", HospitalId);
                throw new NotFoundException("No se encontró el hospital.");
            }

            var response = _mapper.Map<HospitalResponse>(hospitalExisted);

            _logger.LogInformation("Hospital con ID {ChangeHospitalRequestId} recuperada exitosamente", HospitalId);

            return response;
        }
    }
}
