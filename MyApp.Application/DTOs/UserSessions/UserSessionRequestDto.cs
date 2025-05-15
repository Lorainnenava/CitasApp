namespace MyApp.Application.DTOs.UserSessions
{
    public class UserSessionRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
    }
}
