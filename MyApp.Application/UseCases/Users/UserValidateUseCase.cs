using Microsoft.Extensions.Logging;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Users
{
    public class UserValidateUseCase
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
            _logger.LogInformation("Iniciando validación del usuario");

            UsersEntity? searchUser = await _userRepository.GetByCondition(x => x.CodeValidation == code);

            if (searchUser is null)
            {
                _logger.LogWarning("No se encontró ningún usuario con este code {Code}", code);
                throw new NotFoundException("Codigo incorrecto por favor verifique e intente de nuevo.");
            }

            searchUser.CodeValidation = null;

            UsersEntity? updateEntity = await _userRepository.Update(x => x.UserId == searchUser.UserId, searchUser, searchUser);

            return true;
        }
    }
}
