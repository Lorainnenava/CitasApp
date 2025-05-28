using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.ChangeHospitalRequests;
using MyApp.Application.Interfaces.UseCases.ChangeHospitalRequests;
using MyApp.Application.Validators.ChangeHospitalRequest;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Shared.Services;

namespace MyApp.Application.UseCases.ChangeHospitalRequests
{
    public class ChangeHospitalRequestCreateUseCase : IChangeHospitalRequestCreateUseCase
    {
        private readonly IGenericRepository<UsersEntity> _userRepository;
        private readonly IGenericRepository<ChangeHospitalRequestsEntity> _changeHospitalRequestRepository;
        private readonly IGenericRepository<HospitalsEntity> _hospitalRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ChangeHospitalRequestCreateUseCase> _logger;

        public ChangeHospitalRequestCreateUseCase(
            IGenericRepository<UsersEntity> userRepository,
            IMapper mapper,
            ILogger<ChangeHospitalRequestCreateUseCase> logger,
            IGenericRepository<HospitalsEntity> hospitalEntity,
            IGenericRepository<ChangeHospitalRequestsEntity> changeHospitalRequestRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
            _hospitalRepository = hospitalEntity;
            _changeHospitalRequestRepository = changeHospitalRequestRepository;
        }

        public async Task<ChangeHospitalRequestResponse> Execute(ChangeHospitalRequestCreateRequest request)
        {
            _logger.LogInformation("Iniciando creación de solicitud de cambio de hospital para el usuario con IS: {UserId}", request.UserId);

            var validator = new ChangeHospitalRequestCreateValidator();
            ValidatorHelper.ValidateAndThrow(request, validator);

            if (request.CurrentHospitalId == request.NewHospitalId)
            {
                _logger.LogWarning("El hospital actual y el nuevo hospital son el mismo con ID {HospitalId}.", request.CurrentHospitalId);
                throw new ConflictException("El hospital actual y el nuevo hospital no pueden ser el mismo.");
            }

            var user = await _userRepository.GetByCondition(x => x.UserId == request.UserId);

            if (user is null)
            {
                _logger.LogWarning("Intento de crear una solicitud de cambio de hospital para un usuario inexistente con UserId: {UserId}", request.UserId);
                throw new NotFoundException("No se encontró al usuario para registrar la solicitud.");
            }

            var existingRequest = await _changeHospitalRequestRepository.GetByCondition(x => x.UserId == request.UserId);

            if (existingRequest is not null)
            {
                _logger.LogWarning("Ya existe una solicitud de cambio de hospital para el usuario con ID: {UserId}", request.UserId);
                throw new AlreadyExistsException("Ya existe una solicitud de cambio de hospital para el usuario.");
            }

            var currentHospital = await _hospitalRepository.GetByCondition(x => x.HospitalId == request.CurrentHospitalId && x.IsActive);

            if (currentHospital is null)
            {
                _logger.LogWarning("El hospital actual con ID {HospitalId} no existe.", request.CurrentHospitalId);
                throw new NotFoundException("No se encontró el hospital actual para registrar la solicitud.");
            }

            var newHospital = await _hospitalRepository.GetByCondition(x => x.HospitalId == request.NewHospitalId && x.IsActive);

            if (newHospital is null)
            {
                _logger.LogWarning("El nuevo hospital con ID {HospitalId} no existe.", request.NewHospitalId);
                throw new NotFoundException("No se encontró el nuevo hospital seleccionado.");
            }

            var changeHospitalRequest = _mapper.Map<ChangeHospitalRequestsEntity>(request);

            var createdRequest = await _changeHospitalRequestRepository.Create(changeHospitalRequest);

            return _mapper.Map<ChangeHospitalRequestResponse>(createdRequest);
        }
    }
}
