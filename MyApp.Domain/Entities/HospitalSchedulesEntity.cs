using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class HospitalSchedulesEntity
    {
        [Key]
        public int HospitalScheduleId { get; set; }

        [Required]
        public int AppointmentDurationMinutes { get; set; } = 20;

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }
        public bool AllDays { get; set; }
        // Días disponibles
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
    }

}
