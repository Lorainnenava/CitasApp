using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class AppointmentPaymentsEntity
    {
        [Key]
        public int AppointmentPaymentId { get; set; }
        public int AppointmentId { get; set; }
        public double Amount { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public AppointmentsEntity Appointment { get; set; } = null!;
        public PaymentTypesEntity PaymentType { get; set; } = null!;
        public StatusesEntity Status { get; set; } = null!;
    }
}
