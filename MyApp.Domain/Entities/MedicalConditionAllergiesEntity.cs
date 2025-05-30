using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class MedicalConditionAllergiesEntity
    {
        [Key]
        public int MedicalConditionAllergyId { get; set; }
        public int MedicalConditionId { get; set; }
        [ForeignKey("Allergy")]
        public int AllergyId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public AllergiesEntity Allergy { get; set; } = null!;
        public MedicalConditionsEntity MedicalCondition { get; set; } = null!;
    }
}
