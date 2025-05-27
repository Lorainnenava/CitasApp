using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.ChangeHospitalRequests;
using MyApp.Application.Interfaces.UseCases.ChangeHospitalRequests;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.ChangeHospitalRequests
{
    public class ChangeHospitalRequestGetByIdUseCase : IChangeHospitalRequestGetByIdUseCase
    {
        private readonly IGenericRepository<ChangeHospitalRequestsEntity> _changeHospitalRequestRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ChangeHospitalRequestGetByIdUseCase> _logger;

        public ChangeHospitalRequestGetByIdUseCase(
            IGenericRepository<ChangeHospitalRequestsEntity> changeHospitalRequestRepository,
            ILogger<ChangeHospitalRequestGetByIdUseCase> logger,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _changeHospitalRequestRepository = changeHospitalRequestRepository;
        }

        public async Task<ChangeHospitalRequestResponse> Execute(int ChangeHospitalRequestId)
        {
            _logger.LogInformation("Buscando una solicitud de cambio de hospital con ChangeHospitalRequestId: {ChangeHospitalRequestId}", ChangeHospitalRequestId);

            var changeHospitalRequest = await _changeHospitalRequestRepository.GetByCondition(x => x.ChangeHospitalRequestId == ChangeHospitalRequestId);

            if (changeHospitalRequest is null)
            {
                _logger.LogWarning("No se encontró la solicitud de cambio con ID: {ChangeHospitalRequestId}", ChangeHospitalRequestId);
                throw new NotFoundException("No se encontró la solicitud de cambio de hospital.");
            }

            var response = _mapper.Map<ChangeHospitalRequestResponse>(changeHospitalRequest);

            _logger.LogInformation("Solicitud de cambio con ID {ChangeHospitalRequestId} recuperada exitosamente", ChangeHospitalRequestId);

            return response;
        }
    }
}
