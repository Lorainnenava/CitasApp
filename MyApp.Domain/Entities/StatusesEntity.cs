using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class StatusesEntity
    {
        [Key]
        public int StatusId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int StatusTypeId { get; set; }

        // Relaciones
        public StatusTypesEntity StatusType { get; set; }
    }
}
