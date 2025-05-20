namespace MyApp.Domain.Entities
{
    public class MedicalConditionDiseasesEntity
    {
        public int MedicalConditionDiseaseId { get; set; }
        public int MedicalConditionId { get; set; }
        public int DiseaseId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public DiseasesEntity Disease { get; set; } = new();
        public MedicalConditionsEntity MedicalCondition { get; set; } = new();
    }
}
