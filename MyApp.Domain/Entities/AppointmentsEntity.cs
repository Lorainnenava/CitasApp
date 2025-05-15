using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class AppointmentsEntity
    {
        [Key]
        public int AppointmentId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int SpecialtyId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public DateOnly AppointmentDate { get; set; }
        [Required]
        public TimeOnly AppointmentTime { get; set; }
        [Required]
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public UsersEntity User { get; set; } = new();
        public SpecialtiesEntity Specialty { get; set; } = new();
        public DoctorsEntity Doctor { get; set; } = new();
        public StatusesEntity Status { get; set; } = new();
    }
}
