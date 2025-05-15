using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class DiseasesEntity
    {
        [Key]
        public int DiseaseId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        // Relaciones
        public ICollection<MedicalConditionDiseasesEntity> MedicalConditionDiseases { get; set; } = [];
    }
}
