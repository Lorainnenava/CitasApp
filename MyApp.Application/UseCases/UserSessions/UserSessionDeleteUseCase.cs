using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.UserSessions;
using MyApp.Application.Interfaces.UseCases.UserSessions;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.UserSessions
{
    public class UserSessionDeleteUseCase : IUserSessionDeleteUseCase
    {
        private readonly IGenericRepository<RefreshTokensEntity> _refreshTokensRepository;
        private readonly IGenericRepository<UserSessionsEntity> _userSessionsRepository;
        private readonly ILogger<UserSessionDeleteUseCase> _logger;
        public UserSessionDeleteUseCase(
            IGenericRepository<RefreshTokensEntity> refreshTokensRepository,
            IGenericRepository<UserSessionsEntity> userSessionsRepository,
            ILogger<UserSessionDeleteUseCase> logger)
        {
            _logger = logger;
            _userSessionsRepository = userSessionsRepository;
            _refreshTokensRepository = refreshTokensRepository;
        }

        public async Task<bool> Execute(UserSessionResponseDto request)
        {
            _logger.LogInformation("Iniciando proceso para eliminar sesión de usuario con refresh token: {RefreshToken}", request.RefreshToken);

            var searchRefreshToken = await _refreshTokensRepository.GetByCondition(x => x.Token == request.RefreshToken, x => x.Session);

            if (searchRefreshToken is null)
            {
                _logger.LogWarning("No se encontró ningún refresh token con el valor: {RefreshToken}", request.RefreshToken);
                throw new NotFoundException("No existe ningun refresh token con este valor.");
            }

            if (searchRefreshToken.Session is null)
            {
                _logger.LogWarning("No se encontró ninguna sesión con el ID: {Id}", searchRefreshToken.SessionId);
                throw new NotFoundException($"No existe ninguna sessión asociada a este refresh token");
            }

            await _refreshTokensRepository.Delete(x => x.Token == request.RefreshToken);
            await _userSessionsRepository.Delete(x => x.UserSessionId == searchRefreshToken.SessionId);

            _logger.LogInformation("Sesión eliminada exitosamente");
            return true;
        }
    }
}