namespace MyApp.Application.DTOs.HospitalSchedules
{
    public class HospitalScheduleRequest
    {
        public int AppointmentDurationMinutes { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool? AllDays { get; set; }
        public bool? Monday { get; set; }
        public bool? Tuesday { get; set; }
        public bool? Wednesday { get; set; }
        public bool? Thursday { get; set; }
        public bool? Friday { get; set; }
        public bool? Saturday { get; set; }
    }
}
