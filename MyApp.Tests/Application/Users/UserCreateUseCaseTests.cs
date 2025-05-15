using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MyApp.Application.DTOs.Users;
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

        public UserCreateUseCaseTests()
        {
            _userRepositoryMock = new Mock<IGenericRepository<UsersEntity>>();
            _loggerMock = new Mock<ILogger<UserCreateUseCase>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRequest, UsersEntity>();
                cfg.CreateMap<UsersEntity, UserResponse>();
            });
            _mapper = config.CreateMapper();

            _useCase = new UserCreateUseCase(
                _userRepositoryMock.Object,
                _mapper,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldCreateUserSuccessfully()
        {
            var userEntity = MockUser.MockOneUserEntity();
            var userRequest = MockUser.MockOneUserRequest();

            _userRepositoryMock.Setup(x => x.Create(It.IsAny<UsersEntity>())).ReturnsAsync(userEntity);

            var result = await _useCase.Execute(userRequest);

            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
            Assert.Equal("luis@example.com", result.Email);
        }

        [Fact]
        public async Task Execute_ShouldThrowConflictException_WhenEmailAlreadyExists()
        {
            var userEntity = MockUser.MockOneUserEntity();
            var userRequest = MockUser.MockOneUserRequest();

            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync(userEntity);

            var exception = await Assert.ThrowsAsync<ConflictException>(() => _useCase.Execute(userRequest));

            Assert.Equal("El email 'luis@example.com' ya está registrado.", exception.Message);
        }

        [Fact]
        public async Task Execute_ShouldThrowApplicationException_WhenRepositoryFails()
        {
            var userRequest = MockUser.MockOneUserRequest();

            _userRepositoryMock
                .Setup(repo => repo.Create(It.IsAny<UsersEntity>()))
                .ThrowsAsync(new Exception("DB Error"));

            var exception = await Assert.ThrowsAsync<ApplicationException>(() => _useCase.Execute(userRequest));

            Assert.Contains("Ha ocurrido un error en la creación del usuario", exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal("DB Error", exception.InnerException.Message);
        }
    }
}
