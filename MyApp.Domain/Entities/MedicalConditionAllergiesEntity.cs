namespace MyApp.Domain.Entities
{
    public class MedicalConditionAllergiesEntity
    {
        public int MedicalConditionAllergyId { get; set; }
        public int MedicalConditionId { get; set; }
        public int AllergyId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public AllergiesEntity Allergy { get; set; } = new();
        public MedicalConditionsEntity MedicalCondition { get; set; } = new();
    }
}
