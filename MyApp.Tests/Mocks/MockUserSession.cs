using MyApp.Application.DTOs.UserSessions;
using MyApp.Domain.Entities;

namespace MyApp.Tests.Mocks
{
    public class MockUserSession
    {
        public static UserSessionRequestDto MockUserSessionsRequestDto()
        {
            return new UserSessionRequestDto
            {
                Email = "user@example.com",
                Password = "123456",
                IpAddress = "127.0.0.1",
            };
        }

        public static UsersEntity MockUsersEntity()
        {
            return new UsersEntity
            {
                Email = "user@example.com",
                PasswordHash = "123456",
                UserId = 1
            };
        }

        public static UserSessionsEntity MockUserSessionsEntity()
        {
            return new UserSessionsEntity
            {
                UserSessionId = 99,
                UserId = 1
            };
        }

        public static RefreshTokensEntity MockRefreshTokensEntity()
        {
            return new RefreshTokensEntity
            {
                SessionId = 99,
                IsActive = true,
                Token = "refreshToken123",
                TokenExpirationDate = DateTime.UtcNow.AddDays(7),
            };
        }

        public static UserSessionRequestDto MockUserSessionsRequestDtoWrong()
        {
            return new UserSessionRequestDto
            {
                Email = "fake@example.com",
                Password = "wrong",
                IpAddress = "127.0.0.1",
            };
        }
    }
}
