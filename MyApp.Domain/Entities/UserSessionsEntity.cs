using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class UserSessionsEntity
    {
        [Key]
        public int UserSessionId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string IpAddress { get; set; } = string.Empty;
        [Required]
        public bool IsRevoked { get; set; } = false;
        [Required]
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public UsersEntity User { get; set; } = new();
        public RefreshTokensEntity refreshTokensEntity { get; set; } = new();
    }
}