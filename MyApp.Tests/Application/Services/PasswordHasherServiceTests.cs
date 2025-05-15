using MyApp.Application.Services;

namespace MyApp.Tests.Application.Services
{
    public class PasswordHasherServiceTests
    {
        [Fact]
        public void HashPassword_ShouldReturnDifferentHashes_ForSamePassword()
        {
            var password = "MySecurePassword123";

            var hash1 = PasswordHasher.HashPassword(password);
            var hash2 = PasswordHasher.HashPassword(password);

            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_ForCorrectPassword()
        {
            var password = "MySecurePassword123";
            var hash = PasswordHasher.HashPassword(password);

            var result = PasswordHasher.VerifyPassword(password, hash);

            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_ForIncorrectPassword()
        {
            var password = "MySecurePassword123";
            var wrongPassword = "WrongPassword456";
            var hash = PasswordHasher.HashPassword(password);

            var result = PasswordHasher.VerifyPassword(wrongPassword, hash);

            Assert.False(result);
        }
    }
}
