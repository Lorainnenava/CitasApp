using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Users;
using MyApp.Application.Interfaces.Infrastructure;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Application.Validators.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Shared.Services;

namespace MyApp.Application.UseCases.Users
{
    public class UserChangePasswordUseCase : IUserChangePasswordUseCase
    {
        private readonly IGenericRepository<UsersEntity> _userRepository;
        private readonly ILogger<UserChangePasswordUseCase> _logger;
        private readonly IPasswordHasherService _passwordHasherService;

        public UserChangePasswordUseCase(
            IGenericRepository<UsersEntity> userRepository,
            ILogger<UserChangePasswordUseCase> logger,
            IPasswordHasherService passwordHasherService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<bool> Execute(int userId, UserChangePasswordRequest request)
        {
            _logger.LogInformation("Iniciando el cambio de contraseña para el usuario con ID: {UserId}", userId);

            var validator = new UserChangePasswordValidator();
            ValidatorHelper.ValidateAndThrow(request, validator);

            var user = await _userRepository.GetByCondition(u => u.UserId == userId);

            if (user is null)
            {
                _logger.LogWarning("No se encontró ningún usuario con ID: {UserId}", userId);
                throw new NotFoundException("Usuario no encontrado.");
            }

            bool isCurrentPasswordValid = _passwordHasherService.VerifyPassword(request.CurrentPassword, user.PasswordHash);

            if (!isCurrentPasswordValid)
            {
                _logger.LogWarning("Contraseña actual incorrecta para usuario con UserId: {UserId}", userId);
                throw new UnauthorizedAccessException("Contraseña actual incorrecta.");
            }

            user.PasswordHash = _passwordHasherService.HashPassword(request.NewPassword);
            user.UpdatedAt = DateTime.Now;

            await _userRepository.Update(user);

            _logger.LogInformation("Contraseña cambiada exitosamente para el usuario con UserId: {UserId}", userId);

            return true;
        }
    }
}
