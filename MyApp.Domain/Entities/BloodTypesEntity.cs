using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class BloodTypesEntity
    {
        [Key]
        public int BloodTypeId { get; set; }

        [Required]
        [StringLength(5)]
        public string Name { get; set; } = string.Empty;

        public ICollection<MedicalHistoriesEntity> MedicalHistories { get; set; } = [];
    }
}
