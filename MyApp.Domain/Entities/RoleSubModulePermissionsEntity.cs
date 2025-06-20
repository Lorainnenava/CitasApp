using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class RoleSubModulePermissionsEntity
    {
        [Key]
        public int RoleSubModulePermissionId { get; set; }
        public int RoleId { get; set; }
        public int SubModulePermissionId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public RolesEntity Role { get; set; } = null!;
        public SubmodulePermissionsEntity SubModulePermission { get; set; } = null!;
    }
}
