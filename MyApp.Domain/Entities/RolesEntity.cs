using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class RolesEntity
    {
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsSystemDefined { get; set; } = false;

        // Relaciones
        public ICollection<UsersEntity> Users { get; set; } = null!;
    }
}
