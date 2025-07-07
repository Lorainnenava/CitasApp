namespace MyApp.Application.DTOs.ChangeHospitalRequests
{
    public class ChangeHospitalRequestListResponse
    {
        public int ChangeHospitalRequestId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public string CurrentHospitalName { get; set; } = string.Empty;
        public string NewHospitalName { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
