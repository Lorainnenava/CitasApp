namespace MyApp.Domain.Entities
{
    public class ChangeHospitalRequestsEntity
    {
        public int ChangeHospitalRequestId { get; set; }
        public int UserId { get; set; }
        public int CurrentHospitalId { get; set; }
        public int NewHospitalId { get; set; }
        public string ReasonForChange { get; set; } = string.Empty;
        public int StatusId { get; set; } = 13;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Relaciones
        public UsersEntity User { get; set; } = new();
        public HospitalsEntity CurrentHospital { get; set; } = new();
        public HospitalsEntity NewHospital { get; set; } = new();
        public StatusesEntity Status { get; set; } = new();
    }
}