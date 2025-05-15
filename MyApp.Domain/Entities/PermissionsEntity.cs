using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class PermissionsEntity
    {
        [Key]
        public int PermissionId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
