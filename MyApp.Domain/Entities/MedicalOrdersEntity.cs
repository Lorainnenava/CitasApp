namespace MyApp.Domain.Entities
{
    public class MedicalOrdersEntity
    {
        public int MedicalOrderId { get; set; }
        public int AppointmentId { get; set; }
        public int UserId { get; set; }
        public int OrderTypeId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public AppointmentsEntity Appointment { get; set; } = new();
        public UsersEntity User { get; set; } = new();
        public OrderTypesEntity OrderType { get; set; } = new();
        public StatusesEntity Status { get; set; } = new();
    }
}
