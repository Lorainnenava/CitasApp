using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class DoctorsEntity
    {
        [Key]
        public int DoctorId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int SpecialtyId { get; set; }
        [Required]
        [StringLength(30)]
        public string LicenseNumber { get; set; } = string.Empty;
        [Required]
        public int YearsExperience { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsOnVacation { get; set; } = false;
        public DateTime? StartDateVacation { get; set; }
        public DateTime? EndDateVacation { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public UsersEntity User { get; set; } = new();
        public SpecialtiesEntity Specialty { get; set; } = new();
        public ICollection<DoctorSchedulesEntity> DoctorSchedules { get; set; } = [];
    }
}
