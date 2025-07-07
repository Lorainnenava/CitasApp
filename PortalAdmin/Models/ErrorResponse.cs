namespace PortalAdmin.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string? Details { get; set; }
        public List<string>? Errors { get; set; }
    }
}
