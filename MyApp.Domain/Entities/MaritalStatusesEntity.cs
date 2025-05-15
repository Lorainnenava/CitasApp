using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class MaritalStatusesEntity
    {
        [Key]
        public int MaritalStatusId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        // Relaciones
        public ICollection<MedicalHistoriesEntity> MedicalHistories { get; set; } = [];
    }
}
