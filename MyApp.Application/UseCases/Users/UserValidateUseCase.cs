using Microsoft.Extensions.Logging;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

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

        public async Task<bool> Execute(string code)
        {
            _logger.LogInformation("Iniciando validación del usuario por código de verificación.");

            UsersEntity? searchUser = await _userRepository.GetByCondition(x => x.CodeValidation == code);

            if (searchUser is null)
            {
                _logger.LogWarning("No se encontró ningún usuario con este codigo: {Code}", code);
                throw new NotFoundException("Codigo incorrecto por favor verifique e intente nuevamente.");
            }

            searchUser.CodeValidation = null;
            searchUser.IsActive = true;
            searchUser.UpdatedAt = DateTime.Now;

            UsersEntity? updateEntity = await _userRepository.Update(searchUser);

            return true;
        }
    }
}
