using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.DoctorSchedules
{
    public class DoctorScheduleRequest
    {
        [Required]
        public int DoctorId { get; set; }
        public bool AllDays { get; set; } = false;
        public DayOfWeek DayOfWeek { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
    }
}
