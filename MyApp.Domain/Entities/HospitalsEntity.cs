using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class HospitalsEntity
    {
        [Key]
        public int HospitalId { get; set; }
        public int MunicipalityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Nit { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Relaciones
        public MunicipalitiesEntity Municipality { get; set; } = null!;
        public ICollection<UsersEntity> Users { get; set; } = null!;
        public HospitalSchedulesEntity HospitalSchedule { get; set; } = null!;
    }
}