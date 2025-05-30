using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class DoctorSchedulesEntity
    {
        [Key]
        public int DoctorScheduleId { get; set; }
        public int DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public DoctorsEntity Doctor { get; set; } = null!;
    }
}
