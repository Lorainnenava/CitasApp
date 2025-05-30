using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    public class DiagnosesEntity
    {
        [Key]
        public int DiagnosesId { get; set; }
        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public string ConsultationReason { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public AppointmentsEntity Appointment { get; set; } = null!;
    }
}
