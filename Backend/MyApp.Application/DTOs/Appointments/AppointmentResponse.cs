namespace MyApp.Application.DTOs.Appointments
{
    public class AppointmentResponse
    {
        public int UserId { get; set; }
        public int SpecialtyId { get; set; }
        public int DoctorId { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public int StatusId { get; set; }
    }
}
