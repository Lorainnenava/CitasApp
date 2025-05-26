using MyApp.Application.DTOs.UserSessions;
using MyApp.Domain.Entities;

namespace MyApp.Tests.Mocks
{
    public class MockUserSession
    {
        public static UserSessionRequest MockUserSessionsRequestDto()
        {
            return new UserSessionRequest
            {
                Email = "user@example.com",
                Password = "123456",
                IpAddress = "127.0.0.1",
            };
        }

        public static UsersEntity MockUsersEntityCorrect()
        {
            return new UsersEntity
            {
                Email = "user@example.com",
                PasswordHash = "hashed_password_placeholder",
                UserId = 1,
                CodeValidation = null,
                IsActive = true,
            };
        }

        public static UserSessionsEntity MockUserSessionsEntity()
        {
            return new UserSessionsEntity
            {
                UserSessionId = 99,
                UserId = 1,
                IpAddress = "127.0.0.1",
            };
        }

        public static RefreshTokensEntity MockRefreshTokensEntity()
        {
            return new RefreshTokensEntity
            {
                UserSessionId = 99,
                IsActive = true,
                Token = "refreshToken123",
                TokenExpirationDate = DateTime.UtcNow.AddDays(7),
                UserSession =
                {
                    UserSessionId = 101,
                    UserId = 202,
                    IpAddress = "192.168.0.123",
                    IsRevoked = false,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    UpdatedAt = DateTime.UtcNow,
                }
            };
        }

        public static UserSessionRequest MockUserSessionsRequestDtoWrong()
        {
            return new UserSessionRequest
            {
                Email = "fake@example.com",
                Password = "wrong",
                IpAddress = "127.0.0.1",
            };
        }

        public static UsersEntity MockUserSessionsEntityWithNotActive()
        {
            return new UsersEntity
            {
                Email = "test@example.com",
                PasswordHash = "hashed_correct",
                IsActive = true,
                CodeValidation = null
            };
        }
    }
}
