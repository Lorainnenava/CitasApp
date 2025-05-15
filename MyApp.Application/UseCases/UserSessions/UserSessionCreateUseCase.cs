using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.UserSessions;
using MyApp.Application.Interfaces.Infrastructure;
using MyApp.Application.Interfaces.UseCases.UserSessions;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using System.Security.Claims;

namespace MyApp.Application.UseCases.UserSessions
{
    public class UserSessionCreateUseCase : IUserSessionsCreateUseCase
    {
        private readonly IGenericRepository<UsersEntity> _usersRepository;
        private readonly IGenericRepository<UserSessionsEntity> _userSessionsRepository;
        private readonly IGenericRepository<RefreshTokensEntity> _refreshTokensRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly ILogger<UserSessionCreateUseCase> _logger;

        public UserSessionCreateUseCase(
            IGenericRepository<UsersEntity> usersRepository,
            IGenericRepository<UserSessionsEntity> userSessionsRepository,
            IJwtHandler jwtHandler,
            IGenericRepository<RefreshTokensEntity> refreshTokensRepository,
            ILogger<UserSessionCreateUseCase> logger)
        {
            _logger = logger;
            _usersRepository = usersRepository;
            _userSessionsRepository = userSessionsRepository;
            _jwtHandler = jwtHandler;
            _refreshTokensRepository = refreshTokensRepository;
        }

        public async Task<UserSessionResponseDto> Execute(UserSessionRequestDto request)
        {
            var searchUser = await _usersRepository.GetByCondition(x => x.Email == request.Email && x.Password == request.Password);

            if (searchUser is null)
            {
                _logger.LogWarning("El usuario con el email {Request.Email} ingreso credenciales invalidas", request.Email);
                throw new InvalidDataException("Las credenciales son incorrectas.");
            }

            if (searchUser.CodeValidation is not null || searchUser.IsActive is false)
            {
                _logger.LogWarning("El usuario con el email {email} esta inactivo", request.Email);
                throw new NotFoundException("El usuario no existe o no esta activo.");
            }

            var claims = new List<Claim>
                {
                    new(ClaimTypes.Sid, searchUser.UserId.ToString())
                };

            var generateAccessToken = _jwtHandler.GenerateAccessToken(claims);
            var generateRefreshToken = _jwtHandler.GenerateRefreshToken();

            var dataToCreate = new UserSessionsEntity
            {
                UserId = searchUser.UserId,
                IpAddress = request.IpAddress,
                ExpiresAt = generateRefreshToken.TokenExpirationDate,
            };

            var createUserSession = await _userSessionsRepository.Create(dataToCreate);

            generateRefreshToken.SessionId = createUserSession.UserSessionId;
            var createRefreshToken = await _refreshTokensRepository.Create(generateRefreshToken);

            _logger.LogInformation("Sesión creada exitosamente.");

            return new UserSessionResponseDto
            {
                AccessToken = generateAccessToken,
                RefreshToken = generateRefreshToken.Token
            };
        }
    }
}
