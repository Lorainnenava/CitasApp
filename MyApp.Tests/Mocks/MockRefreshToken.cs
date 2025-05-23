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
                SessionId = 1,
                Token = "myRefreshToken",
                TokenExpirationDate = DateTime.UtcNow.AddMinutes(10),
                IsActive = true,
            };
        }

        public static RefreshTokensEntity MockRefreshTokenEntityExpired()
        {
            return new RefreshTokensEntity
            {
                RefreshTokenId = 1,
                SessionId = 1,
                Token = "expiredToken",
                IsActive = false,
                TokenExpirationDate = DateTime.UtcNow.AddMinutes(-1),
            };
        }
    }
}
