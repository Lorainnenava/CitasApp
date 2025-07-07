namespace MyApp.Application.DTOs.DoctorSchedules
{
    public class DoctorScheduleResponse
    {
        public int DoctorScheduleId { get; set; }
        public int DoctorId { get; set; }
        public bool AllDays { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
