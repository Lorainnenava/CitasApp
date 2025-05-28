using MyApp.Application.DTOs.HospitalSchedules;

namespace MyApp.Application.DTOs.Hospitals
{
    public class HospitalResponse
    {
        public int HospitalId { get; set; }
        public int MunicipalityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public HospitalScheduleResponse HospitalSchedule { get; set; } = new();
    }
}
