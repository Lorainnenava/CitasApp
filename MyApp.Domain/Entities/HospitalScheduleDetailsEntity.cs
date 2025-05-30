using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class HospitalScheduleDetailsEntity
    {
        [Key]
        public int HospitalScheduleDetailId { get; set; }
        public int HospitalScheduleId { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        // Relación
        public HospitalSchedulesEntity HospitalSchedule { get; set; } = null!;
    }
}
