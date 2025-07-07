using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class ChangeHospitalRequestsEntity
    {
        [Key]
        public int ChangeHospitalRequestId { get; set; }
        public int UserId { get; set; }
        public int CurrentHospitalId { get; set; }
        public int NewHospitalId { get; set; }
        public string ReasonForChange { get; set; } = string.Empty;
        public int StatusId { get; set; } = 13;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Relaciones
        public UsersEntity User { get; set; } = null!;
        public HospitalsEntity CurrentHospital { get; set; } = null!;
        public HospitalsEntity NewHospital { get; set; } = null!;
        public StatusesEntity Status { get; set; } = null!;
    }
}