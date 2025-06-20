namespace MyApp.Application.DTOs.Hospitals
{
    public class HospitalListResponse
    {
        public int HospitalId { get; set; }
        public string MunicipalityName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
