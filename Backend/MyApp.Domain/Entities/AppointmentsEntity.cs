using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class AppointmentsEntity
    {
        [Key]
        public int AppointmentId { get; set; }
        public int UserId { get; set; }
        public int HospitalId { get; set; }
        public int SpecialtyId { get; set; }
        public int DoctorId { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public UsersEntity User { get; set; } = null!;
        public HospitalsEntity Hospital { get; set; } = null!;
        public SpecialtiesEntity Specialty { get; set; } = null!;
        public DoctorsEntity Doctor { get; set; } = null!;
        public StatusesEntity Status { get; set; } = null!;
        public DiagnosesEntity? Diagnose { get; set; } = null!;
        public PrescriptionsEntity? Prescription { get; set; } = null!;
        public MedicalOrdersEntity? MedicalOrder { get; set; } = null!;
    }
}
