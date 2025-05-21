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
    public class UserGetByIdUseCaseTests
    {
        public readonly Mock<IGenericRepository<UsersEntity>> _userRepositoryMock;
        public readonly IMapper _mapper;
        private readonly UserGetByIdUseCase _useCase;
        private readonly Mock<ILogger<UserGetByIdUseCase>> _loggerMock;

        public UserGetByIdUseCaseTests()
        {
            _userRepositoryMock = new Mock<IGenericRepository<UsersEntity>>();
            _loggerMock = new Mock<ILogger<UserGetByIdUseCase>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserCreateRequest, UsersEntity>();
                cfg.CreateMap<UsersEntity, UserResponse>();
            });
            _mapper = config.CreateMapper();
            _useCase = new UserGetByIdUseCase(_userRepositoryMock.Object, _mapper, _loggerMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnUserByIdSuccessfully()
        {
            var expectedUser = MockUser.MockOneUserEntity();

            _userRepositoryMock.Setup(x => x.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync(expectedUser);

            var result = await _useCase.Execute(1);

            Assert.NotNull(result);
            Assert.Equal("NuevoUsuario", result.UserName);
            Assert.Equal("luis@example.com", result.Email);
        }

        [Fact]
        public async Task Execute_WithNonExistentUser_ReturnsNull()
        {
            _ = _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync(null as UsersEntity);

            await Assert.ThrowsAsync<NotFoundException>(() => _useCase.Execute(99));
        }


        [Fact]
        public async Task Execute_ShouldThrowApplicationException_WhenGetByIdFails()
        {
            var userRequest = MockUser.MockOneUserRequest();

            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ThrowsAsync(new Exception("DB Error"));

            var exception = await Assert.ThrowsAsync<ApplicationException>(() => _useCase.Execute(1));

            Assert.Contains("Ha ocurrido un error al obtener un usuario", exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal("DB Error", exception.InnerException.Message);
        }
    }
}
