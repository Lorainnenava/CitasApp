using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class StatusTypesEntity
    {
        [Key]
        public int StatusTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
