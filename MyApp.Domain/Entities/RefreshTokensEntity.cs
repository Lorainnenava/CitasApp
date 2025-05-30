using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class RefreshTokensEntity
    {
        [Key]
        public int RefreshTokenId { get; set; }
        public int UserSessionId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime TokenExpirationDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public UserSessionsEntity UserSession { get; set; } = null!;
    }
}
