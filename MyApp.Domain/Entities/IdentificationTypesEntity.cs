using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class IdentificationTypesEntity
    {
        [Key]
        public int IdentificationTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        // Relaciones
        public ICollection<UsersEntity> Users { get; set; } = [];
    }
}
