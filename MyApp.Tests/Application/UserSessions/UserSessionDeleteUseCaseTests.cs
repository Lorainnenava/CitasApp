using Microsoft.Extensions.Logging;
using Moq;
using MyApp.Application.DTOs.UserSessions;
using MyApp.Application.UseCases.UserSessions;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using System.Linq.Expressions;

namespace MyApp.Tests.Application.UserSessions
{
    public class UserSessionDeleteUseCaseTests
    {
        private readonly Mock<IGenericRepository<RefreshTokensEntity>> _refreshTokenRepoMock;
        private readonly Mock<IGenericRepository<UserSessionsEntity>> _userSessionRepoMock;
        private readonly Mock<ILogger<UserSessionDeleteUseCase>> _loggerMock;
        private readonly UserSessionDeleteUseCase _useCase;

        public UserSessionDeleteUseCaseTests()
        {
            _refreshTokenRepoMock = new Mock<IGenericRepository<RefreshTokensEntity>>();
            _userSessionRepoMock = new Mock<IGenericRepository<UserSessionsEntity>>();
            _loggerMock = new Mock<ILogger<UserSessionDeleteUseCase>>();

            _useCase = new UserSessionDeleteUseCase(
                _refreshTokenRepoMock.Object,
                _userSessionRepoMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task Execute_WithValidData_ReturnsTrue()
        {
            var request = new UserSessionResponseDto { RefreshToken = "valid-token" };
            var refreshToken = new RefreshTokensEntity { Token = "valid-token", UserId = 1 };
            var userSession = new UserSessionsEntity { UserId = 1 };

            _refreshTokenRepoMock
                .Setup(r => r.GetByCondition(It.IsAny<Expression<Func<RefreshTokensEntity, bool>>>()))
                .ReturnsAsync(refreshToken);

            _userSessionRepoMock
                .Setup(r => r.GetByCondition(It.IsAny<Expression<Func<UserSessionsEntity, bool>>>()))
                .ReturnsAsync(userSession);

            _refreshTokenRepoMock
                .Setup(r => r.Delete(It.IsAny<Expression<Func<RefreshTokensEntity, bool>>>()))
                .ReturnsAsync(true);

            _userSessionRepoMock
                .Setup(r => r.Delete(It.IsAny<Expression<Func<UserSessionsEntity, bool>>>()))
                .ReturnsAsync(true);

            var result = await _useCase.Execute(request);

            Assert.True(result);
        }

        [Fact]
        public async Task Execute_RefreshTokenNotFound_ThrowsException()
        {
            var request = new UserSessionResponseDto { RefreshToken = "invalid-token" };

            _refreshTokenRepoMock
                .Setup(r => r.GetByCondition(x => x.Token == It.IsAny<string>()))
                .ReturnsAsync((RefreshTokensEntity)null!);

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _useCase.Execute(request));
            Assert.Contains("No existe ningun refresh token con este valor", ex.InnerException?.Message);
        }

        [Fact]
        public async Task Execute_UserSessionNotFound_ThrowsException()
        {
            var request = new UserSessionResponseDto { RefreshToken = "token" };
            var refreshToken = new RefreshTokensEntity { Token = "token", UserId = 2 };

            _refreshTokenRepoMock
                .Setup(r => r.GetByCondition(It.IsAny<Expression<Func<RefreshTokensEntity, bool>>>()))
                .ReturnsAsync(refreshToken);

            _userSessionRepoMock
                .Setup(r => r.GetByCondition(x => x.UserId == It.IsAny<int>()))
                .ReturnsAsync((UserSessionsEntity)null!);

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _useCase.Execute(request));
            Assert.Contains("No existe ningun UserId con una sessión activa", ex.InnerException?.Message);
        }

        [Fact]
        public async Task Execute_WhenUnexpectedErrorOccurs_ThrowsApplicationException()
        {
            var request = new UserSessionResponseDto { RefreshToken = "token" };
            var exceptionMessage = "DB error";
            _refreshTokenRepoMock
                .Setup(r => r.GetByCondition(It.IsAny<Expression<Func<RefreshTokensEntity, bool>>>()))
                .ThrowsAsync(new Exception("DB error"));

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _useCase.Execute(request));
            Assert.Contains("Ha ocurrido un error", ex.Message);
            Assert.Equal("DB error", ex.InnerException?.Message);
        }
    }
}
