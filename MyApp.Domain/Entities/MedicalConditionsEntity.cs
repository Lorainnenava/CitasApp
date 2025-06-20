using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class MedicalConditionsEntity
    {
        [Key]
        public int MedicalConditionId { get; set; }
        [ForeignKey("MedicalHistory")]
        public int MedicalHistoryId { get; set; }
        public string CurrentMedications { get; set; } = string.Empty;
        public bool CovidVaccines { get; set; }
        public int CovidVaccineDoses { get; set; }
        public string FamilyDiseases { get; set; } = string.Empty;
        public string Surgeries { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relación
        public MedicalHistoriesEntity MedicalHistory { get; set; } = null!;
        public ICollection<MedicalConditionDiseasesEntity> MedicalConditionDiseases { get; set; } = [];
        public ICollection<MedicalConditionAllergiesEntity> MedicalConditionAllergies { get; set; } = [];
    }
}
