using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class RefreshTokensEntity
    {
        [Key]
        public int RefreshTokenId { get; set; }
        [Required]
        public int SessionId { get; set; }
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public DateTime TokenExpirationDate { get; set; }
        [Required]
        public bool Active { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public UserSessionsEntity Session { get; set; }
    }
}
