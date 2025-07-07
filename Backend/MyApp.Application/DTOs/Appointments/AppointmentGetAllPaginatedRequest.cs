using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Appointments
{
    public class AppointmentGetAllPaginatedRequest
    {
        [Required]
        public int SpecialtyId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateOnly AppointmentDate { get; set; }
    }
}
