using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.UserSessions;
using MyApp.Application.Interfaces.Infrastructure;
using MyApp.Application.Interfaces.UseCases.RefreshTokens;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using System.Security.Claims;

namespace MyApp.Application.UseCases.RefreshTokens
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IGenericRepository<RefreshTokensEntity> _refreshTokensRepository;
        private readonly IGenericRepository<UserSessionsEntity> _userSessionsRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly ILogger<RefreshTokenService> _logger;

        public RefreshTokenService(
            IGenericRepository<RefreshTokensEntity> refreshTokensRepository,
            IGenericRepository<UserSessionsEntity> userSessionsRepository,
            IJwtHandler jwtHandler,
            ILogger<RefreshTokenService> logger)
        {
            _logger = logger;
            _jwtHandler = jwtHandler;
            _userSessionsRepository = userSessionsRepository;
            _refreshTokensRepository = refreshTokensRepository;
        }

        public async Task<UserSessionResponseDto> Execute(UserSessionResponseDto request)
        {
            _logger.LogInformation("Iniciando la actualización de token para refresh token: {RefreshToken}", request.RefreshToken);

            var searchRefreshToken = await _refreshTokensRepository.GetByCondition(x => x.Token == request.RefreshToken, x => x.Session);

            if (searchRefreshToken is null || !searchRefreshToken.Active || searchRefreshToken.TokenExpirationDate <= DateTime.UtcNow)
            {
                _logger.LogWarning("Refresh token inválido o expirado. Token: {RefreshToken}", request.RefreshToken);

                if (searchRefreshToken?.SessionId is not null)
                {
                    await _userSessionsRepository.Delete(x => x.UserSessionId == searchRefreshToken.SessionId);
                    await _refreshTokensRepository.Delete(x => x.SessionId == searchRefreshToken.SessionId);

                    _logger.LogInformation("Sesión y token eliminados para SessionId: {SessionId}", searchRefreshToken.SessionId);
                }

                throw new UnauthorizedAccessException("La sessión expiro");
            }

            var claims = new List<Claim>
                {
                    new(ClaimTypes.Sid, searchRefreshToken.Session.UserId.ToString())
                };

            var generateAccessToken = _jwtHandler.GenerateAccessToken(claims);

            _logger.LogInformation("Nuevo token generado exitosamente para el usuario {UserId}", searchRefreshToken.Session.UserId);

            return new UserSessionResponseDto { AccessToken = generateAccessToken, RefreshToken = request.RefreshToken };
        }
    }
}
