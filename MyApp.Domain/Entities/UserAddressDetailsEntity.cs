using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class UserAddressDetailsEntity
    {
        [Key]
        public int AddressId { get; set; }
        [ForeignKey("MedicalHistory")]
        public int MedicalHistoryId { get; set; }
        public int MunicipalityId { get; set; }
        public string Address { get; set; } = string.Empty;
        public int ResidentsCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relación
        public MedicalHistoriesEntity MedicalHistory { get; set; } = null!;
        public MunicipalitiesEntity Municipality { get; set; } = null!;
    }
}
