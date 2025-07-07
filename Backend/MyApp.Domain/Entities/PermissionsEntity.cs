using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class PermissionsEntity
    {
        [Key]
        public int PermissionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
