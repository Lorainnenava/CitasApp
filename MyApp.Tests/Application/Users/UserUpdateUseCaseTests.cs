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
    public class UserUpdateUseCaseTests
    {
        public readonly Mock<IGenericRepository<UsersEntity>> _userRepositoryMock;
        public readonly IMapper _mapper;
        private readonly UserUpdateUseCase _useCase;
        private readonly Mock<ILogger<UserUpdateUseCase>> _loggerMock;

        public UserUpdateUseCaseTests()
        {
            _userRepositoryMock = new Mock<IGenericRepository<UsersEntity>>();
            _loggerMock = new Mock<ILogger<UserUpdateUseCase>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserCreateRequest, UsersEntity>();
                cfg.CreateMap<UsersEntity, UserResponse>();
            });
            _mapper = config.CreateMapper();
            _useCase = new UserUpdateUseCase(_userRepositoryMock.Object, _mapper, _loggerMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnUserUpdatedSuccessfully()
        {
            var userToUpdate = MockUser.MockOneUserEntityToUpdate();
            var expectedUser = MockUser.MockOneUserEntityUpdated();

            _userRepositoryMock.Setup(x => x.Update(It.IsAny<Expression<Func<UsersEntity, bool>>>(), It.IsAny<UsersEntity>()))
                .ReturnsAsync(expectedUser);

            var result = await _useCase.Execute(1, userToUpdate);

            Assert.NotNull(result);
            Assert.Equal("DevJane", result.UserName);
            Assert.Equal("jane.doe567@example.com", result.Email);
        }

        [Fact]
        public async Task Execute_ShouldThrowApplicationException_WhenUpdateFails()
        {
            var userToUpdate = MockUser.MockOneUserEntityToUpdate();

            _userRepositoryMock
                .Setup(repo => repo.Update(It.IsAny<Expression<Func<UsersEntity, bool>>>(), It.IsAny<UsersEntity>()))
                .ThrowsAsync(new Exception("DB Error"));

            var exception = await Assert.ThrowsAsync<ApplicationException>(() => _useCase.Execute(1, userToUpdate));

            Assert.Contains("Ha ocurrido un error en la actualización del usuario", exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal("DB Error", exception.InnerException.Message);
        }
    }
}
