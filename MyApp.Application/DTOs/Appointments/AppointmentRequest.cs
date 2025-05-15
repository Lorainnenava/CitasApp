using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Appointments
{
    public class AppointmentRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int SpecialtyId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateOnly AppointmentDate { get; set; }

        [Required]
        public TimeOnly AppointmentTime { get; set; }

        [Required]
        public int StatusId { get; set; }
    }
}
