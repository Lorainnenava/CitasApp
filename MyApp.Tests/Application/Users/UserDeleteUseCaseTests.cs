using Microsoft.Extensions.Logging;
using Moq;
using MyApp.Application.UseCases.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using System.Linq.Expressions;

namespace MyApp.Tests.Application.Users
{
    public class UserDeleteUseCaseTests
    {
        private readonly Mock<IGenericRepository<UsersEntity>> _userRepositoryMock;
        private readonly UserDeleteUseCase _useCase;
        private readonly Mock<ILogger<UserDeleteUseCase>> _loggerMock;

        public UserDeleteUseCaseTests()
        {
            _userRepositoryMock = new Mock<IGenericRepository<UsersEntity>>();
            _loggerMock = new Mock<ILogger<UserDeleteUseCase>>();

            _useCase = new UserDeleteUseCase(_userRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnTrue_WhenUserIsSuccessfullyDeleted()
        {
            _userRepositoryMock
                .Setup(repo => repo.Delete(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync(true);

            var result = await _useCase.Execute(1);

            Assert.True(result);
        }

        [Fact]
        public async Task Execute_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            _userRepositoryMock
                .Setup(repo => repo.Delete(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync(false);

            await Assert.ThrowsAsync<NotFoundException>(() => _useCase.Execute(99));
        }

        [Fact]
        public async Task Execute_ShouldThrowApplicationException_WhenRepositoryFails()
        {
            _userRepositoryMock
                .Setup(repo => repo.Delete(x => x.UserId == 1))
                .ThrowsAsync(new Exception("DB Error"));

            var exception = await Assert.ThrowsAsync<ApplicationException>(() => _useCase.Execute(1));

            Assert.Contains("Ha ocurrido un error en la eliminación del usuario", exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal("DB Error", exception.InnerException.Message);
        }
    }
}
