using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class RolePermissionsEntity
    {
        [Key]
        public int RolePermissionId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int PermissionId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public RolesEntity Role { get; set; }
        public PermissionsEntity Permission { get; set; }
    }
}
