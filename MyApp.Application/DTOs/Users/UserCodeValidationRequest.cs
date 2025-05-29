namespace MyApp.Application.DTOs.Users
{
    public class UserCodeValidationRequest
    {
        public string Email { get; set; } = string.Empty;
        public string CodeValidation { get; set; } = string.Empty;
    }
}
