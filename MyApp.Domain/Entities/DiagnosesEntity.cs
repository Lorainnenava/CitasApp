using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class DiagnosesEntity
    {
        [Key]
        public int DiagnosesId { get; set; }
        [Required]
        public int AppointmentId { get; set; }
        [Required]
        [StringLength(200)]
        public string ConsultationReason { get; set; } = string.Empty;
        [StringLength(100)]
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public AppointmentsEntity Appointment { get; set; } = new();
    }
}
