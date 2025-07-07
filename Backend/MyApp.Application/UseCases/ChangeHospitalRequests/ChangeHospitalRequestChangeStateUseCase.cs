using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.ChangeHospitalRequests;
using MyApp.Application.Enums;
using MyApp.Application.Interfaces.UseCases.ChangeHospitalRequests;
using MyApp.Application.Validators.ChangeHospitalRequest;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Shared.Services;

namespace MyApp.Application.UseCases.ChangeHospitalRequests
{
    public class ChangeHospitalRequestChangeStateUseCase : IChangeHospitalRequestChangeStateUseCase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<UsersEntity> _userRepository;
        private readonly ILogger<ChangeHospitalRequestChangeStateUseCase> _logger;
        private readonly IGenericRepository<ChangeHospitalRequestsEntity> _changeHospitalRequestRepository;

        public ChangeHospitalRequestChangeStateUseCase(
            IMapper mapper,
            IGenericRepository<UsersEntity> userRepository,
            ILogger<ChangeHospitalRequestChangeStateUseCase> logger,
            IGenericRepository<ChangeHospitalRequestsEntity> changeHospitalRequestRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
            _changeHospitalRequestRepository = changeHospitalRequestRepository;
        }

        public async Task<ChangeHospitalRequestResponse> Execute(ChangeHospitalRequestChangeStateRequest request)
        {
            _logger.LogInformation("Iniciando creación de solicitud de cambio de hospital para la solicitud con el ID: {ID}", request.ChangeHospitalRequestId);

            var validator = new ChangeHospitalRequestChangeStateValidator();
            ValidatorHelper.ValidateAndThrow(request, validator);

            var changeRequest = await _changeHospitalRequestRepository.GetByCondition(x => x.ChangeHospitalRequestId == request.ChangeHospitalRequestId);

            if (changeRequest is null)
            {
                _logger.LogWarning("Intento de actualizar una solicitud de cambio de hospital inexistente con ID: {RequestId}", request.ChangeHospitalRequestId);
                throw new NotFoundException("No se encontró la solicitud de cambio de hospital.");
            }

            if ((StatusIdEnum)changeRequest.StatusId != StatusIdEnum.EnEstudio &&
                ((StatusIdEnum)request.StatusId == StatusIdEnum.Rechazada || (StatusIdEnum)request.StatusId == StatusIdEnum.Aceptada))
            {
                _logger.LogWarning("No se puede cambiar el estado. Solicitud ID: {RequestId}, Estado actual: {CurrentStatus}", request.ChangeHospitalRequestId, changeRequest.StatusId);
                throw new ConflictException("Solo se puede aceptar o rechazar si está en estudio.");
            }

            var user = await _userRepository.GetByCondition(x => x.UserId == changeRequest.UserId);

            if (user is null)
            {
                _logger.LogWarning("Intento de actualizar una solicitud de cambio de hospital para un usuario inexistente con UserId: {UserId}", changeRequest.UserId);
                throw new NotFoundException("No se encontró al usuario para actualizar la solicitud.");
            }

            if ((StatusIdEnum)request.StatusId == StatusIdEnum.Aceptada)
            {
                _logger.LogInformation("Actualizando el hospital del usuario con ID: {UserId} al nuevo hospital con ID: {NewHospitalId}", user!.UserId, changeRequest.NewHospitalId);
                user!.HospitalId = changeRequest.NewHospitalId;
                var updateUser = await _userRepository.Update(user);
            }

            var changeHospitalRequest = _mapper.Map<ChangeHospitalRequestsEntity>(request);

            changeHospitalRequest.UpdatedAt = DateTime.UtcNow;

            var createdRequest = await _changeHospitalRequestRepository.Update(changeRequest, changeHospitalRequest);

            _logger.LogInformation("Estado de la solicitud de cambio de hospital actualizado con éxito. Solicitud ID: {RequestId}", createdRequest.ChangeHospitalRequestId);

            var response = _mapper.Map<ChangeHospitalRequestResponse>(createdRequest);

            return response;
        }
    }
}
