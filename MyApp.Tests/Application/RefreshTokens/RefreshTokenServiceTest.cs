using Microsoft.Extensions.Logging;
using Moq;
using MyApp.Application.DTOs.UserSessions;
using MyApp.Application.Interfaces.Infrastructure;
using MyApp.Application.UseCases.RefreshTokens;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Tests.Mocks;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MyApp.Tests.Application.RefreshTokens
{
    public class RefreshTokenServiceTests
    {
        private readonly Mock<IGenericRepository<RefreshTokensEntity>> _refreshTokenRepoMock;
        private readonly Mock<IGenericRepository<UserSessionsEntity>> _userSessionsRepoMock;
        private readonly Mock<IJwtHandler> _jwtHandlerMock;
        private readonly RefreshTokenService _service;
        private readonly Mock<ILogger<RefreshTokenService>> _loggerMock;

        public RefreshTokenServiceTests()
        {
            _refreshTokenRepoMock = new Mock<IGenericRepository<RefreshTokensEntity>>();
            _userSessionsRepoMock = new Mock<IGenericRepository<UserSessionsEntity>>();
            _jwtHandlerMock = new Mock<IJwtHandler>();
            _loggerMock = new Mock<ILogger<RefreshTokenService>>();

            _service = new RefreshTokenService(
                _refreshTokenRepoMock.Object,
                _userSessionsRepoMock.Object,
                _jwtHandlerMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task Execute_WithValidRefreshToken_ReturnsNewAccessToken()
        {
            var refreshToken = MockRefreshToken.MockRefreshTokenEntity();

            _refreshTokenRepoMock
                .Setup(r => r.GetByCondition(It.IsAny<Expression<Func<RefreshTokensEntity, bool>>>()))
                .ReturnsAsync(refreshToken);

            _jwtHandlerMock
                .Setup(j => j.GenerateAccessToken(It.IsAny<List<Claim>>()))
                .Returns("newAccessToken");

            var request = new UserSessionResponse { RefreshToken = "myRefreshToken" };

            var result = await _service.Execute(request);

            Assert.Equal("newAccessToken", result.AccessToken);
            Assert.Equal("myRefreshToken", result.RefreshToken);
        }

        [Fact]
        public async Task Execute_WithExpiredRefreshToken_DeletesSessionAndThrowsException()
        {
            var refreshToken = MockRefreshToken.MockRefreshTokenEntityExpired();

            _refreshTokenRepoMock
                .Setup(r => r.GetByCondition(x => x.Token == It.IsAny<string>()))
                .ReturnsAsync(refreshToken);

            _userSessionsRepoMock
                .Setup(r => r.Delete(x => x.UserSessionId == It.IsAny<int>()))
                .ReturnsAsync(true);

            _refreshTokenRepoMock
                .Setup(r => r.Delete(x => x.Token == It.IsAny<string>()))
                .ReturnsAsync(true);

            var request = new UserSessionResponse { RefreshToken = "expiredToken" };

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _service.Execute(request));
            Assert.Contains("La sessión expiro", ex.InnerException?.Message);
        }

        [Fact]
        public async Task Execute_WhenRepositoryThrowsException_ThrowsApplicationException()
        {
            _refreshTokenRepoMock
                .Setup(r => r.GetByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<RefreshTokensEntity, bool>>>()))
                .ThrowsAsync(new Exception("DB failure"));

            var request = new UserSessionResponse { RefreshToken = "anyToken" };

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _service.Execute(request));
            Assert.Contains("Ha ocurrido un error", ex.Message);
            Assert.Equal("DB failure", ex.InnerException?.Message);
        }
    }
}
