using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class AllergiesEntity
    {
        [Key]
        public int AllergyId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        // Relaciones
        public ICollection<MedicalConditionAllergiesEntity> MedicalConditionAllergies { get; set; } = [];
    }
}
