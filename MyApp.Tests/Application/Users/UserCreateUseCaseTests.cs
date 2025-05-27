using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MyApp.Application.DTOs.Users;
using MyApp.Application.Interfaces.Infrastructure;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.UseCases.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Tests.Mocks;
using System.Linq.Expressions;

namespace MyApp.Tests.Application.Users
{
    public class UserCreateUseCaseTests
    {
        public readonly Mock<IGenericRepository<UsersEntity>> _userRepositoryMock;
        public readonly IMapper _mapper;
        private readonly UserCreateUseCase _useCase;
        private readonly Mock<ILogger<UserCreateUseCase>> _loggerMock;
        private readonly Mock<ICodeGeneratorService> _codeGeneratorServiceMock;
        private readonly Mock<IPasswordHasherService> _passwordHasherServiceMock;
        private readonly Mock<IGenericRepository<HospitalsEntity>> _hospitalRepositoryMock;

        public UserCreateUseCaseTests()
        {
            _userRepositoryMock = new Mock<IGenericRepository<UsersEntity>>();
            _loggerMock = new Mock<ILogger<UserCreateUseCase>>();
            _codeGeneratorServiceMock = new Mock<ICodeGeneratorService>();
            _passwordHasherServiceMock = new Mock<IPasswordHasherService>();
            _hospitalRepositoryMock = new Mock<IGenericRepository<HospitalsEntity>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserCreateRequest, UsersEntity>();
                cfg.CreateMap<UsersEntity, UserResponse>();
            });
            _mapper = config.CreateMapper();

            _useCase = new UserCreateUseCase(
                _userRepositoryMock.Object,
                _mapper,
                _loggerMock.Object,
                _passwordHasherServiceMock.Object,
                _codeGeneratorServiceMock.Object,
                _hospitalRepositoryMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldCreateUserSuccessfully()
        {
            var userEntity = MockUser.MockOneUserEntityWithCodeValidation();
            var userRequest = MockUser.MockOneUserRequest();
            var hospitalResponse = MockHospital.MockHospitalEntity();

            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync((UsersEntity)null!);

            _codeGeneratorServiceMock
                .Setup(service => service.GenerateUniqueCode())
                .ReturnsAsync("CODE123");

            _passwordHasherServiceMock
                .Setup(service => service.HashPassword(userRequest.Password))
                .Returns("hashed_password_placeholder");

            _hospitalRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<HospitalsEntity, bool>>>()))
                .ReturnsAsync(hospitalResponse);

            _userRepositoryMock
                .Setup(x => x.Create(It.IsAny<UsersEntity>()))
                .ReturnsAsync(userEntity);

            var result = await _useCase.Execute(userRequest);

            Assert.NotNull(result);
            Assert.Equal(userEntity.UserId, result.UserId);
            Assert.Equal(userEntity.Email, result.Email);
        }

        [Fact]
        public async Task Execute_ShouldThrowConflictException_WhenEmailAlreadyExists()
        {
            var userEntity = MockUser.MockOneUserEntity();
            var userRequest = MockUser.MockOneUserRequest();

            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync(userEntity);

            var exception = await Assert.ThrowsAsync<AlreadyExistsException>(() => _useCase.Execute(userRequest));

            Assert.Equal("El email 'usuario.prueba@example.com' ya está registrado.", exception.Message);
        }

        [Fact]
        public async Task Execute_ShouldThrowConflictException_WhenHospitalNotExists()
        {
            var userRequest = MockUser.MockOneUserRequest();

            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync((UsersEntity)null!);

            _hospitalRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<HospitalsEntity, bool>>>()))
                .ReturnsAsync((HospitalsEntity)null!);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _useCase.Execute(userRequest));

            Assert.Equal("El hospital con el HospitalId '1' no existe.", exception.Message);
        }

        [Fact]
        public async Task Execute_ShouldThrowValidationException_WhenRequestIsInvalid()
        {
            var userRequest = new UserCreateRequest
            {
                Email = "",
                Password = "123"
            };

            await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => _useCase.Execute(userRequest));
        }
    }
}
