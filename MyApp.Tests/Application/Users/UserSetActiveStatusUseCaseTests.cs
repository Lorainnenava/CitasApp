using Microsoft.Extensions.Logging;
using Moq;
using MyApp.Application.UseCases.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Tests.Mocks;
using System.Linq.Expressions;

namespace MyApp.Tests.Application.Users
{
    public class UserSetActiveStatusUseCaseTests
    {
        private readonly Mock<IGenericRepository<UsersEntity>> _userRepositoryMock;
        private readonly UserSetActiveStatusUseCase _useCase;
        private readonly Mock<ILogger<UserSetActiveStatusUseCase>> _loggerMock;

        public UserSetActiveStatusUseCaseTests()
        {
            _userRepositoryMock = new Mock<IGenericRepository<UsersEntity>>();
            _loggerMock = new Mock<ILogger<UserSetActiveStatusUseCase>>();

            _useCase = new UserSetActiveStatusUseCase(_userRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldToggleIsActive_WhenUserExists()
        {
            var userEntity = MockUser.MockOneUserEntity();

            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync(userEntity);

            _userRepositoryMock
                .Setup(repo => repo.Update(It.IsAny<UsersEntity>()))
                .ReturnsAsync(userEntity);

            var result = await _useCase.Execute(1);

            Assert.True(result);
            Assert.False(userEntity.IsActive);
            _userRepositoryMock.Verify(repo => repo.Update(It.Is<UsersEntity>(u => u.UserId == 1 && u.IsActive == false)), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync((UsersEntity)null!);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _useCase.Execute(99));
            Assert.Equal("Usuario no encontrado.", exception.Message);
        }
    }
}
