using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class DoctorSchedulesEntity
    {
        [Key]
        public int DoctorScheduleId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        public bool? AllDays { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public DoctorsEntity Doctor { get; set; } = new();
    }
}
