using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class DoctorVacationsEntity
    {
        [Key]
        public int DoctorVacationId { get; set; }
        public int DoctorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public DoctorsEntity Doctor { get; set; } = null!;
    }
}
