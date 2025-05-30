using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class UserSessionsEntity
    {
        [Key]
        public int UserSessionId { get; set; }
        public int UserId { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public bool IsRevoked { get; set; } = false;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public UsersEntity User { get; set; } = null!;
        public RefreshTokensEntity RefreshTokenEntity { get; set; } = null!;
    }
}