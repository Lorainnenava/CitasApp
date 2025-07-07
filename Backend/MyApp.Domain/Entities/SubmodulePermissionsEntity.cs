using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class SubmodulePermissionsEntity
    {
        [Key]
        public int SubModulePermissionId { get; set; }
        public int SubModuleId { get; set; }
        public int PermissionId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public SubModulesEntity SubModule { get; set; } = null!;
        public PermissionsEntity Permission { get; set; } = null!;
        public ICollection<RoleSubModulePermissionsEntity> RoleSubModulePermissions { get; set; } = null!;
    }
}
