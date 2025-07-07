using MyApp.Application.DTOs.DoctorSchedules;

namespace MyApp.Application.DTOs.Doctors
{
    public class DoctorResponse
    {
        public int DoctorId { get; set; }
        public int SpecialtyId { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public int YearsExperience { get; set; }
        public bool IsActive { get; set; } = true;
        public List<DoctorScheduleResponse> DoctorSchedules { get; set; } = [];
    }
}
