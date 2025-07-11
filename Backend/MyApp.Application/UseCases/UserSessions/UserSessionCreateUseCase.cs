﻿using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.UserSessions;
using MyApp.Application.Interfaces.Infrastructure;
using MyApp.Application.Interfaces.UseCases.UserSessions;
using MyApp.Application.Validators.UserSessions;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Shared.Services;
using System.Security.Claims;

namespace MyApp.Application.UseCases.UserSessions
{
    public class UserSessionCreateUseCase : IUserSessionsCreateUseCase
    {
        private readonly IJwtHandler _jwtHandler;
        private readonly ILogger<UserSessionCreateUseCase> _logger;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IGenericRepository<UsersEntity> _usersRepository;
        private readonly IGenericRepository<UserSessionsEntity> _userSessionsRepository;
        private readonly IGenericRepository<RefreshTokensEntity> _refreshTokensRepository;
        private readonly IUserSessionRevokedUseCase _userSessionRevokedUseCase;
        private readonly IGenericRepository<RoleSubModulePermissionsEntity> _roleSubModulePermissionRepository;

        public UserSessionCreateUseCase(
            IGenericRepository<UsersEntity> usersRepository,
            IGenericRepository<UserSessionsEntity> userSessionsRepository,
            IJwtHandler jwtHandler,
            IGenericRepository<RefreshTokensEntity> refreshTokensRepository,
            IPasswordHasherService passwordHasherService,
            IUserSessionRevokedUseCase userSessionRevokedUseCase,
            IGenericRepository<RoleSubModulePermissionsEntity> roleSubModulePermissionRepository,
            ILogger<UserSessionCreateUseCase> logger)
        {
            _logger = logger;
            _jwtHandler = jwtHandler;
            _usersRepository = usersRepository;
            _passwordHasherService = passwordHasherService;
            _userSessionsRepository = userSessionsRepository;
            _refreshTokensRepository = refreshTokensRepository;
            _userSessionRevokedUseCase = userSessionRevokedUseCase;
            _roleSubModulePermissionRepository = roleSubModulePermissionRepository;
        }

        public async Task<UserSessionResponse> Execute(UserSessionRequest request)
        {
            var validator = new UserSessionCreateValidator();
            ValidatorHelper.ValidateAndThrow(request, validator);

            var searchUser = await _usersRepository.GetByCondition(x => x.Email == request.Email);

            if (searchUser is null)
            {
                _logger.LogWarning("El usuario con el email {Email} ingresó credenciales inválidas", request.Email);
                throw new InvalidDataException("Email o contraseña incorrectos. Por favor, intenta de nuevo.");
            }

            if (searchUser.CodeValidation is not null || !searchUser.IsActive)
            {
                _logger.LogWarning("El usuario con el email {Email} está inactivo o no ha validado su cuenta", request.Email);
                throw new NotFoundException("Cuenta inactiva o no verificada.");
            }

            bool isPasswordValid = _passwordHasherService.VerifyPassword(request.Password, searchUser.PasswordHash);

            if (!isPasswordValid)
            {
                _logger.LogWarning("El usuario con el email {Email} ingresó una contraseña incorrecta", request.Email);
                throw new InvalidDataException("Email o contraseña incorrectos. Por favor, intenta de nuevo.");
            }

            var hasActiveSession = await _userSessionsRepository.GetByCondition(x => x.UserId == searchUser.UserId && x.IsRevoked == false, x => x.RefreshTokenEntity);

            if (hasActiveSession is not null)
            {
                await _userSessionRevokedUseCase.Execute(hasActiveSession.RefreshTokenEntity.Token);
            }

            var roleSubModulePermission = await this._roleSubModulePermissionRepository.GetAll(
                x => x.RoleId == searchUser.RoleId,
                x => x.SubModulePermission.SubModule,
                x => x.SubModulePermission.Permission);

            if (roleSubModulePermission == null || !roleSubModulePermission.Any())
            {
                _logger.LogWarning("El usuario con el rol {roleId} no tiene permisos asignados", searchUser.RoleId);
                throw new NotFoundException("No se encontraron permisos para el rol asignado a este usuario.");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, searchUser.UserId.ToString()),
                new(ClaimTypes.Role, searchUser.Role.Name),
            };

            foreach (var permission in roleSubModulePermission)
            {
                var route = permission.SubModulePermission.SubModule.Route?.ToUpperInvariant();
                var action = permission.SubModulePermission.Permission.Code?.ToUpperInvariant();

                if (!string.IsNullOrWhiteSpace(route) && !string.IsNullOrWhiteSpace(action))
                {
                    var claimValue = $"{route}_{action}";
                    claims.Add(new Claim("Permission", claimValue));
                }
            }

            var generateAccessToken = _jwtHandler.GenerateAccessToken(claims);
            var generateRefreshToken = await _jwtHandler.GenerateRefreshToken();

            var dataToCreate = new UserSessionsEntity
            {
                UserId = searchUser.UserId,
                IpAddress = request.IpAddress,
                ExpiresAt = generateRefreshToken.TokenExpirationDate,
            };

            var createUserSession = await _userSessionsRepository.Create(dataToCreate);

            generateRefreshToken.UserSessionId = createUserSession.UserSessionId;
            var createRefreshToken = await _refreshTokensRepository.Create(generateRefreshToken);

            _logger.LogInformation("Sesión creada exitosamente para el usuario {Email}", request.Email);

            return new UserSessionResponse
            {
                AccessToken = generateAccessToken,
                RefreshToken = generateRefreshToken.Token
            };
        }
    }
}
