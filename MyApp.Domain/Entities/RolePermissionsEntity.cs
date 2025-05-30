using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class RolePermissionsEntity
    {
        [Key]
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public RolesEntity Role { get; set; } = null!;
        public PermissionsEntity Permission { get; set; } = null!;
    }
}
