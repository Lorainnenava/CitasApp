using MyApp.Application.DTOs.HospitalSchedules;

namespace MyApp.Application.DTOs.Hospitals
{
    public class HospitalUpdateRequest
    {
        public int MunicipalityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public HospitalScheduleRequest HospitalSchedule { get; set; } = new();
    }
}
