using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.HospitalSchedules
{
    public class HospitalScheduleRequest
    {
        [Required]
        public int AppointmentDurationMinutes { get; set; } = 20;

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }
        public bool AllDays { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
    }
}
