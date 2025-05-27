namespace MyApp.Application.DTOs.ChangeHospitalRequests
{
    public class ChangeHospitalRequestResponse
    {
        public int ChangeHospitalRequestId { get; set; }
        public int UserId { get; set; }
        public int CurrentHospitalId { get; set; }
        public int NewHospitalId { get; set; }
        public string ReasonForChange { get; set; } = string.Empty;
        public int StatusId { get; set; }
    }
}
