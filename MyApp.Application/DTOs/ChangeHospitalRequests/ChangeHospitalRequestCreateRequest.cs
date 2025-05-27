namespace MyApp.Application.DTOs.ChangeHospitalRequests
{
    public class ChangeHospitalRequestCreateRequest
    {
        public int UserId { get; set; }
        public int CurrentHospitalId { get; set; }
        public int NewHospitalId { get; set; }
        public string ReasonForChange { get; set; } = string.Empty;
    }
}
