using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class MedicalConditionAllergiesEntity
    {
        [Key]
        public int MedicalConditionAllergyId { get; set; }
        [Required]
        public int MedicalConditionId { get; set; }
        public int? AllergyId { get; set; }
        [StringLength(50)]
        public string? OtherAllergyName { get; set; }
        [StringLength(50)]
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public AllergiesEntity Allergy { get; set; } = new();
    }
}
