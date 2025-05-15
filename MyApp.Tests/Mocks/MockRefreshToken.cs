using MyApp.Domain.Entities;

namespace MyApp.Tests.Mocks
{
    public class MockRefreshToken
    {
        public static RefreshTokensEntity MockRefreshTokenEntity()
        {
            return new RefreshTokensEntity
            {
                RefreshTokenId = 1,
                UserId = 1,
                SessionId = 1,
                Token = "myRefreshToken",
                TokenExpirationDate = DateTime.UtcNow.AddMinutes(10),
                Active = true,
            };
        }

        public static RefreshTokensEntity MockRefreshTokenEntityExpired()
        {
            return new RefreshTokensEntity
            {
                RefreshTokenId = 1,
                UserId = 1,
                SessionId = 1,
                Token = "expiredToken",
                Active = false,
                TokenExpirationDate = DateTime.UtcNow.AddMinutes(-1),
            };
        }
    }
}
