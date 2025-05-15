using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class AppointmentPaymentsEntity
    {
        [Key]
        public int AppointmentPaymentId { get; set; }
        [Required]
        public int AppointmentId { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public int PaymentTypeId { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public AppointmentsEntity Appointment { get; set; } = new();
        public PaymentTypesEntity PaymentType { get; set; } = new();
        public StatusesEntity Status { get; set; } = new();
    }
}
