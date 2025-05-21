using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Users;
using MyApp.Application.Interfaces.Infrastructure;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Users
{
    public class UserCreateUseCase : IUserCreateUseCase
    {
        private readonly IGenericRepository<UsersEntity> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserCreateUseCase> _logger;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly ICodeGeneratorService _codeGeneratorService;

        public UserCreateUseCase(
            IGenericRepository<UsersEntity> userRepository,
            IMapper mapper,
            ILogger<UserCreateUseCase> logger,
            IPasswordHasherService passwordHasherService,
            ICodeGeneratorService codeGeneratorService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasherService = passwordHasherService;
            _codeGeneratorService = codeGeneratorService;
        }

        public async Task<UserResponse> Execute(UserCreateRequest request)
        {
            _logger.LogInformation("Iniciando la creación de usuario con email: {Email}", request.Email);

            var emailExisted = await _userRepository.GetByCondition(x => x.Email == request.Email);

            if (emailExisted != null)
            {
                _logger.LogWarning("Intento de crear usuario con un email ya existente: {Email}", request.Email);
                throw new AlreadyExistsException($"El email '{request.Email}' ya está registrado.");
            }

            var entityMapped = _mapper.Map<UsersEntity>(request);

            var codeValidation = await _codeGeneratorService.GenerateUniqueCode();

            entityMapped.Password = _passwordHasherService.HashPassword(request.Password);
            entityMapped.CodeValidation = codeValidation;

            var userCreated = await _userRepository.Create(entityMapped);

            var response = _mapper.Map<UserResponse>(userCreated);

            _logger.LogInformation("Usuario creado exitosamente con UserId: {UserId}", userCreated.UserId);

            return response;
        }
    }
}
