using Microsoft.Extensions.Logging;
using Moq;
using MyApp.Application.Interfaces.Infrastructure;
using MyApp.Application.UseCases.UserSessions;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Tests.Mocks;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MyApp.Tests.Application.UserSessions
{
    public class UserSessionCreateUseCaseTests
    {
        private readonly Mock<IGenericRepository<UsersEntity>> _usersRepoMock;
        private readonly Mock<IGenericRepository<UserSessionsEntity>> _userSessionsRepoMock;
        private readonly Mock<IGenericRepository<RefreshTokensEntity>> _refreshTokensRepoMock;
        private readonly Mock<IJwtHandler> _jwtHandlerMock;
        private readonly UserSessionCreateUseCase _useCase;
        private readonly Mock<ILogger<UserSessionCreateUseCase>> _loggerMock;

        public UserSessionCreateUseCaseTests()
        {
            _usersRepoMock = new Mock<IGenericRepository<UsersEntity>>();
            _userSessionsRepoMock = new Mock<IGenericRepository<UserSessionsEntity>>();
            _refreshTokensRepoMock = new Mock<IGenericRepository<RefreshTokensEntity>>();
            _jwtHandlerMock = new Mock<IJwtHandler>();
            _loggerMock = new Mock<ILogger<UserSessionCreateUseCase>>();

            _useCase = new UserSessionCreateUseCase(
                _usersRepoMock.Object,
                _userSessionsRepoMock.Object,
                _jwtHandlerMock.Object,
                _refreshTokensRepoMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task Execute_WithValidCredentials_ReturnsAccessAndRefreshToken()
        {
            var request = MockUserSession.MockUserSessionsRequestDto();

            var user = MockUserSession.MockUsersEntity();

            var createdSession = MockUserSession.MockUserSessionsEntity();

            var refreshToken = MockUserSession.MockRefreshTokensEntity();

            _usersRepoMock
                .Setup(r => r.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync(user);

            _jwtHandlerMock
                .Setup(j => j.GenerateAccessToken(It.IsAny<List<Claim>>()))
                .Returns("accessToken123");

            _jwtHandlerMock
                .Setup(j => j.GenerateRefreshToken(1))
                .Returns(refreshToken);

            _userSessionsRepoMock
                .Setup(r => r.Create(It.IsAny<UserSessionsEntity>()))
                .ReturnsAsync(createdSession);

            _refreshTokensRepoMock
                .Setup(r => r.Create(It.IsAny<RefreshTokensEntity>()))
                .ReturnsAsync(refreshToken);

            var result = await _useCase.Execute(request);

            Assert.Equal("accessToken123", result.AccessToken);
            Assert.Equal("refreshToken123", result.RefreshToken);
        }

        [Fact]
        public async Task Execute_WithInvalidCredentials_ThrowsException()
        {
            var request = MockUserSession.MockUserSessionsRequestDtoWrong();

            _usersRepoMock
                .Setup(r => r.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync((UsersEntity)null!);

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _useCase.Execute(request));

            Assert.Contains("Las credenciales son incorrectas", ex.InnerException?.Message);
        }

        [Fact]
        public async Task Execute_WhenRepositoryThrows_ThrowsApplicationException()
        {
            var request = MockUserSession.MockUserSessionsRequestDtoWrong();

            _usersRepoMock
                .Setup(r => r.GetByCondition(It.IsAny<Expression<Func<UsersEntity, bool>>>()))
                .ThrowsAsync(new Exception("DB error"));

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _useCase.Execute(request));
            Assert.Contains("Ha ocurrido un error al momento de inciar sesión", ex.Message);
            Assert.Equal("DB error", ex.InnerException?.Message);
        }
    }
}
