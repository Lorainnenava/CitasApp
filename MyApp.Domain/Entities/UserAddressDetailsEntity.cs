using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class UserAddressDetailsEntity
    {
        [Key]
        public int AddressId { get; set; }
        [Required]
        public int MedicalHistoryId { get; set; }
        [Required]
        public int MunicipalityId { get; set; }
        [Required]
        [StringLength(50)]
        public string Address { get; set; } = string.Empty;
        [Required]
        public int ResidentsCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relación
        public MedicalHistoriesEntity MedicalHistory { get; set; }
        public MunicipalitiesEntity Municipality { get; set; }
    }
}
