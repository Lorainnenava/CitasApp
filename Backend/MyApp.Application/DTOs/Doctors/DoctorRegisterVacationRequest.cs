using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Doctors
{
    public class DoctorRegisterVacationRequest
    {
        [Required]
        public DateTime StartDateVacation { get; set; }
        [Required]
        public DateTime EndDateVacation { get; set; }
    }
}
