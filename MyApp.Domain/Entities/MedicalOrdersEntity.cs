using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    public class MedicalOrdersEntity
    {
        [Key]
        public int MedicalOrderId { get; set; }
        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public int UserId { get; set; }
        public int OrderTypeId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public AppointmentsEntity Appointment { get; set; } = null!;
        public UsersEntity User { get; set; } = null!;
        public OrderTypesEntity OrderType { get; set; } = null!;
        public StatusesEntity Status { get; set; } = null!;
    }
}
