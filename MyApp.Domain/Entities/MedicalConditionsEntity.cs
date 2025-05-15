using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class MedicalConditionsEntity
    {
        [Key]
        public int MedicalConditionId { get; set; }
        [Required]
        public int MedicalHistoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string CurrentMedications { get; set; } = string.Empty;
        [Required]
        public bool CovidVaccines { get; set; }
        [Required]
        public int CovidVaccineDoses { get; set; }
        [Required]
        [StringLength(50)]
        public string FamilyDiseases { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Surgeries { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relación
        public MedicalHistoriesEntity MedicalHistory { get; set; }
    }
}
