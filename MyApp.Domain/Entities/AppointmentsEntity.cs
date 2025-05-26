namespace MyApp.Domain.Entities
{
    public class AppointmentsEntity
    {
        public int AppointmentId { get; set; }
        public int UserId { get; set; }
        public int HospitalId { get; set; }
        public int SpecialtyId { get; set; }
        public int DoctorId { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public UsersEntity User { get; set; } = new();
        public SpecialtiesEntity Specialty { get; set; } = new();
        public DoctorsEntity Doctor { get; set; } = new();
        public StatusesEntity Status { get; set; } = new();
        public DiagnosesEntity Diagnose { get; set; } = new();
        public PrescriptionsEntity Prescription { get; set; } = new();
        public MedicalOrdersEntity MedicalOrder { get; set; } = new();
    }
}
