using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class PrescriptionsEntity
    {
        [Key]
        public int PrescriptionId { get; set; }
        [Required]
        public int AppointmentId { get; set; }
        [Required]
        [StringLength(50)]
        public string MedicationName { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Dosage { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Frequency { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Duration { get; set; } = string.Empty;
        [StringLength(100)]
        public string? Notes { get; set; } = string.Empty;
        [Required]
        public DateTime PrescriptionDate { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public AppointmentsEntity Appointment { get; set; }
    }
}
