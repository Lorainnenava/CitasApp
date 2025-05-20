namespace MyApp.Domain.Entities
{
    public class DoctorsEntity
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public int SpecialtyId { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public int YearsExperience { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public UsersEntity User { get; set; } = new();
        public SpecialtiesEntity Specialty { get; set; } = new();
        public ICollection<DoctorSchedulesEntity> DoctorSchedules { get; set; } = [];
        public ICollection<DoctorVacationsEntity> DoctorVacations { get; set; } = [];
    }
}
