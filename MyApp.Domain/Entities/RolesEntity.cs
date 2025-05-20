namespace MyApp.Domain.Entities
{
    public class RolesEntity
    {
        public int RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public ICollection<UsersEntity> Users { get; set; } = [];
    }
}
