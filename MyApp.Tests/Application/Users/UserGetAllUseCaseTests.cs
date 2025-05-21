using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MyApp.Application.DTOs.Users;
using MyApp.Application.UseCases.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Tests.Mocks;
using System.Linq.Expressions;

namespace MyApp.Tests.Application.Users
{
    public class UserGetAllUseCaseTests
    {
        public readonly Mock<IGenericRepository<UsersEntity>> _userRepositoryMock;
        public readonly IMapper _mapper;
        private readonly UserGetAllPaginatedUseCase _useCase;
        private readonly Mock<ILogger<UserGetAllPaginatedUseCase>> _loggerMock;

        public UserGetAllUseCaseTests()
        {
            _userRepositoryMock = new Mock<IGenericRepository<UsersEntity>>();
            _loggerMock = new Mock<ILogger<UserGetAllPaginatedUseCase>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserCreateRequest, UsersEntity>();
                cfg.CreateMap<UsersEntity, UserResponse>();
            });
            _mapper = config.CreateMapper();
            _useCase = new UserGetAllPaginatedUseCase(_userRepositoryMock.Object, _mapper, _loggerMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnAllUsersSuccessfully()
        {
            var usersEntity = MockUser.MockListUsersEntity();

            _userRepositoryMock.Setup(x => x.GetAll(It.IsAny<Expression<Func<UsersEntity, bool>>>())).ReturnsAsync(usersEntity);

            var result = await _useCase.Execute();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Collection(result,
                user =>
                {
                    Assert.Equal("Prueba123", user.UserName);
                },
                user =>
                {
                    Assert.Equal("jane.doe@example.com", user.Email);
                }
            );
        }

        [Fact]
        public async Task Execute_ShouldReturnAnEmptyListSuccessfully()
        {
            _userRepositoryMock.Setup(x => x.GetAll(null)).ReturnsAsync([]);

            var result = await _useCase.Execute();

            Assert.Empty(result);
            Assert.Equal([], result);
        }

        [Fact]
        public async Task Execute_ShouldThrowApplicationException_WhenRepositoryThrows()
        {
            var userRequest = MockUser.MockOneUserRequest();

            _userRepositoryMock
                .Setup(repo => repo.GetAll(null))
                .ThrowsAsync(new Exception("DB Error"));

            var exception = await Assert.ThrowsAsync<ApplicationException>(() => _useCase.Execute());

            Assert.Contains("Ha ocurrido un error al obtener todos los usuarios", exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal("DB Error", exception.InnerException.Message);
        }
    }
}
