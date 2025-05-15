using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class SpecialtiesEntity
    {
        [Key]
        public int SpecialtyId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
