using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class HospitalSpecialtiesEntity
    {
        [Key]
        public int HospitalSpecialtyId { get; set; }
        public int HospitalId { get; set; }
        public int SpecialtyId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public HospitalsEntity Hospital { get; set; } = null!;
        public SpecialtiesEntity Specialty { get; set; } = null!;
    }
}
