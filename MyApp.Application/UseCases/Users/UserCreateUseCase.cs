using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Users;
using MyApp.Application.Interfaces.Infrastructure;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Application.Validators.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Shared.Services;

namespace MyApp.Application.UseCases.Users
{
    public class UserCreateUseCase : IUserCreateUseCase
    {
        private readonly IGenericRepository<UsersEntity> _userRepository;
        private readonly IGenericRepository<HospitalsEntity> _hospitalRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserCreateUseCase> _logger;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly ICodeGeneratorService _codeGeneratorService;

        public UserCreateUseCase(
            IGenericRepository<UsersEntity> userRepository,
            IMapper mapper,
            ILogger<UserCreateUseCase> logger,
            IPasswordHasherService passwordHasherService,
            ICodeGeneratorService codeGeneratorService,
            IGenericRepository<HospitalsEntity> hospitalEntity)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
            _hospitalRepository = hospitalEntity;
            _passwordHasherService = passwordHasherService;
            _codeGeneratorService = codeGeneratorService;
        }

        public async Task<UserResponse> Execute(UserCreateRequest request)
        {
            _logger.LogInformation("Iniciando la creación de usuario con email: {Email}", request.Email);

            var validator = new UserCreateValidator();
            ValidatorHelper.ValidateAndThrow(request, validator);

            var emailExisted = await _userRepository.GetByCondition(x => x.Email == request.Email);

            if (emailExisted is not null)
            {
                _logger.LogWarning("Intento de crear usuario con un email ya existente: {Email}", request.Email);
                throw new AlreadyExistsException($"El email '{request.Email}' ya está registrado.");
            }

            var hospitalExisted = await _hospitalRepository.GetByCondition(x => x.HospitalId == request.HospitalId);

            if (hospitalExisted is null)
            {
                _logger.LogWarning("Intento de crear usuario con un HospitalId no existente: {HospitalId}", request.HospitalId);
                throw new NotFoundException($"El hospital con el HospitalId '{request.HospitalId}' no existe.");
            }

            var entityMapped = _mapper.Map<UsersEntity>(request);

            var codeValidation = await _codeGeneratorService.GenerateUniqueCode();

            entityMapped.CodeValidation = codeValidation;
            entityMapped.PasswordHash = _passwordHasherService.HashPassword(request.Password);

            var userCreated = await _userRepository.Create(entityMapped);

            var response = _mapper.Map<UserResponse>(userCreated);

            _logger.LogInformation("Usuario creado exitosamente con UserId: {UserId}", userCreated.UserId);

            return response;
        }
    }
}
