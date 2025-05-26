using FluentValidation;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.UserSessions;
using MyApp.Application.Interfaces.Infrastructure;
using MyApp.Application.Interfaces.UseCases.RefreshTokens;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
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

        public async Task<UserSessionResponse> Execute(string RefreshToken)
        {
            _logger.LogInformation("Iniciando la actualización de token para refresh token: {RefreshToken}", RefreshToken);

            if (string.IsNullOrWhiteSpace(RefreshToken))
            {
                _logger.LogWarning("Refresh token no proporcionado o vacío.");
                throw new ValidationException("El refresh token es requerido.");
            }

            var searchRefreshToken = await _refreshTokensRepository.GetByCondition(x => x.Token == RefreshToken, x => x.UserSession);

            if (searchRefreshToken is null || searchRefreshToken.IsActive == false)
            {
                _logger.LogWarning("Refresh token no encontrado: {RefreshToken}", RefreshToken);
                throw new NotFoundException($"El refresh token '{RefreshToken}' no existe.");
            }

            if (searchRefreshToken!.TokenExpirationDate <= DateTime.UtcNow)
            {
                _logger.LogWarning("Refresh token inválido o expirado. Token: {RefreshToken}", RefreshToken);

                if (searchRefreshToken?.UserSessionId is not null)
                {
                    searchRefreshToken.IsActive = false;
                    searchRefreshToken.UserSession.IsRevoked = false;
                    await _userSessionsRepository.Update(searchRefreshToken.UserSession);
                    await _refreshTokensRepository.Update(searchRefreshToken);

                    _logger.LogInformation("Sesión y token eliminados para UserSessionId: {UserSessionId}", searchRefreshToken.UserSessionId);
                }

                throw new UnauthorizedAccessException("La sessión ha expirado.");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, searchRefreshToken.UserSession.UserId.ToString())
            };

            var generateAccessToken = _jwtHandler.GenerateAccessToken(claims);

            _logger.LogInformation("Nuevo token generado exitosamente para el usuario con el UserId: {UserId}", searchRefreshToken.UserSession.UserId);

            return new UserSessionResponse { AccessToken = generateAccessToken, RefreshToken = RefreshToken };
        }
    }
}
