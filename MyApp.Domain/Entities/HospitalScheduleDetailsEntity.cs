namespace MyApp.Domain.Entities
{
    public class HospitalScheduleDetailsEntity
    {
        public int HospitalScheduleDetailId { get; set; }
        public int HospitalScheduleId { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        // Relación
        public HospitalSchedulesEntity HospitalSchedule { get; set; } = new();
    }
}
