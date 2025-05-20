namespace MyApp.Domain.Entities
{
    public class PermissionsEntity
    {
        public int PermissionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
