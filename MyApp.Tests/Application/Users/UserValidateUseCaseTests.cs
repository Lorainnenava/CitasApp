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
    public class UserValidateUseCaseTests
    {
        private readonly Mock<IGenericRepository<UsersEntity>> _userRepositoryMock;
        private readonly UserValidateUseCase _useCase;
        private readonly Mock<ILogger<UserValidateUseCase>> _loggerMock;

        public UserValidateUseCaseTests()
        {
            _userRepositoryMock = new Mock<IGenericRepository<UsersEntity>>();
            _loggerMock = new Mock<ILogger<UserValidateUseCase>>();

            _useCase = new UserValidateUseCase(_userRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldValidateUser_WhenCodeAndEmailExist()
        {
            var userEntity = MockUser.MockOneUserEntityWithCodeValidation();
            var response = MockUser.MockOneUserEntityUpdated();
            var request = MockUser.MockUserValidateRequest();

            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync(userEntity);

            _userRepositoryMock
                .Setup(repo => repo.Update(It.IsAny<UsersEntity>()))
                .ReturnsAsync(response);

            var result = await _useCase.Execute(request);

            Assert.True(result);
            Assert.True(userEntity.IsActive);
            _userRepositoryMock.Verify(repo => repo.Update(It.Is<UsersEntity>(u => u.UserId == 1 && u.IsActive == false)), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldThrowNotFoundException_WhenCodeAndEmailDoNotExist()
        {
            var request = MockUser.MockUserValidateRequest();

            _userRepositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync((UsersEntity)null!);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _useCase.Execute(request));
            Assert.Equal("El código ingresado no es válido. Por favor, intenta nuevamente.", exception.Message);
        }

        [Fact]
        public async Task Execute_ShouldThrowValidationException_WhenRequestIsInvalid()
        {
            var userRequest = MockUser.MockUserValidateInvalidCredentials();

            await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => _useCase.Execute(userRequest));
        }
    }
}
