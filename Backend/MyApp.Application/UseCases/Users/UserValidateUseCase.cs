using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Users;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Application.Validators.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Shared.Services;

namespace MyApp.Application.UseCases.Users
{
    public class UserValidateUseCase : IUserValidateUseCase
    {
        private readonly IGenericRepository<UsersEntity> _userRepository;
        private readonly ILogger<UserValidateUseCase> _logger;

        public UserValidateUseCase(
            IGenericRepository<UsersEntity> userRepository,
            ILogger<UserValidateUseCase> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<bool> Execute(UserCodeValidationRequest request)
        {
            _logger.LogInformation("Iniciando validación del usuario por código de verificación.");

            var validator = new UserCodeValidationValidator();
            ValidatorHelper.ValidateAndThrow(request, validator);

            UsersEntity? searchUser = await _userRepository.GetByCondition(x => x.CodeValidation == request.CodeValidation && x.Email == request.Email);

            if (searchUser is null)
            {
                _logger.LogWarning("Intento fallido de validación con código {CodeValidation} y correo {Email}.", request.CodeValidation, request.Email);
                throw new NotFoundException("El código ingresado no es válido. Por favor, intenta nuevamente.");
            }

            searchUser.CodeValidation = null;
            searchUser.IsActive = true;
            searchUser.UpdatedAt = DateTime.Now;

            UsersEntity? updateEntity = await _userRepository.Update(searchUser);

            _logger.LogInformation("Usuario con código de verificación {CodeValidation} validado exitosamente.", request.CodeValidation);

            return true;
        }
    }
}
