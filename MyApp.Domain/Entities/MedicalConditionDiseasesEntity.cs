using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class MedicalConditionDiseasesEntity
    {
        [Key]
        public int MedicalConditionDiseaseId { get; set; }
        public int MedicalConditionId { get; set; }
        public int DiseaseId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public DiseasesEntity Disease { get; set; } = null!;
        public MedicalConditionsEntity MedicalCondition { get; set; } = null!;
    }
}
