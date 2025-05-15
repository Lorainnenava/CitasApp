using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class MunicipalitiesEntity
    {
        [Key]
        public int MunicipalityId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        // Relaciones
        public ICollection<UserAddressDetailsEntity> UserAddressDetails { get; set; } = [];
    }
}
