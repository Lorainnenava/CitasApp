using Microsoft.Extensions.Logging;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Users
{
    public class UserDeleteUseCase : IUserDeleteUseCase
    {
        private readonly IGenericRepository<UsersEntity> _userRepository;
        private readonly ILogger<UserDeleteUseCase> _logger;

        public UserDeleteUseCase(
            IGenericRepository<UsersEntity> userRepository,
            ILogger<UserDeleteUseCase> logger
            )
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<bool> Execute(int Id)
        {
            _logger.LogInformation("Intentando eliminar usuario con UserId: {UserId}", Id);

            UsersEntity? searchUser = await _userRepository.GetByCondition(u => u.UserId == Id);

            if (searchUser is null)
            {
                _logger.LogWarning("No se encontró ningún usuario con UserId {UserId}", Id);
                throw new NotFoundException("Usuario no encontrado");
            }

            var deleteUser = await _userRepository.Delete(u => u.UserId == Id);

            _logger.LogWarning("No se encontró usuario con UserId: {UserId} para eliminar", Id);

            return deleteUser;
        }
    }
}
