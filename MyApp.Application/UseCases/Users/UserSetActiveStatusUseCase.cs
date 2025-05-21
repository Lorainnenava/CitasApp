using Microsoft.Extensions.Logging;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Users
{
    public class UserSetActiveStatusUseCase : IUserSetActiveStatusUseCase
    {
        private readonly IGenericRepository<UsersEntity> _userRepository;
        private readonly ILogger<UserSetActiveStatusUseCase> _logger;

        public UserSetActiveStatusUseCase(
            IGenericRepository<UsersEntity> userRepository,
            ILogger<UserSetActiveStatusUseCase> logger
            )
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<bool> Execute(int UserId)
        {
            _logger.LogInformation("Intentando cambiar el estado activo/inactivo del usuario con UserId: {UserId}", UserId);

            UsersEntity? searchUser = await _userRepository.GetByCondition(u => u.UserId == UserId);

            if (searchUser is null)
            {
                _logger.LogWarning("No se encontró ningún usuario con UserId: {UserId}", UserId);
                throw new NotFoundException("Usuario no encontrado.");
            }

            searchUser.IsActive = !searchUser.IsActive;

            var updateEntity = await _userRepository.Update(searchUser);

            _logger.LogInformation("Usuario con UserId: {UserId} ahora está {Estado}", UserId, updateEntity.IsActive ? "activo" : "inactivo");

            return true;
        }
    }
}
