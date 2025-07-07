using MyApp.Application.DTOs.DoctorSchedules;
using MyApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Doctors
{
    public class DoctorRequest
    {
        [Required]
        public int SpecialtyId { get; set; }

        [Required]
        [StringLength(30)]
        public string LicenseNumber { get; set; } = string.Empty;

        [Required]
        public int YearsExperience { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public List<DoctorScheduleRequest> DoctorSchedules { get; set; } = [];
    }
}
