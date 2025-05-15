using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class MedicalOrdersEntity
    {
        [Key]
        public int MedicalOrderId { get; set; }
        [Required]
        public int AppointmentId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int OrderTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string OrderDescription { get; set; } = string.Empty;
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public AppointmentsEntity Appointment { get; set; }
        public UsersEntity User { get; set; }
        public OrderTypesEntity OrderType { get; set; }
        public StatusesEntity Status { get; set; }
    }
}
