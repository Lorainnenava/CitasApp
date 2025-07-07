namespace PortalAdmin.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; } = string.Empty;
        public int HospitalId { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string? SecondName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public int IdentificationTypeId { get; set; }
        public int GenderId { get; set; }
        public DateTime? DateOfBirth { get; set; } = null;
        public int RoleId { get; set; }
        public string Phone { get; set; } = string.Empty;
    }
}
