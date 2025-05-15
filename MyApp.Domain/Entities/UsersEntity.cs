using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class UsersEntity
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Length(4, 10)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Length(6, 11)]
        public string IdentificatiónNumber { get; set; } = string.Empty;
        [Required]
        public int IdentificationTypeId { get; set; }
        [Required]
        public int GenderId { get; set; }
        [Required]
        public int RoleId { get; set; }
        public string? CodeValidation { get; set; }
        public bool IsActive { get; set; } = false;
        [Required]
        [MaxLength(10)]
        public string Phone { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public IdentificationTypesEntity IdentificationType { get; set; }
        public GendersEntity Gender { get; set; }
        public RolesEntity Role { get; set; }
    }
}
