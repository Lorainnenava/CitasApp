namespace MyApp.Domain.Entities
{
    public class RolePermissionsEntity
    {
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public RolesEntity Role { get; set; } = new();
        public PermissionsEntity Permission { get; set; } = new();
    }
}
