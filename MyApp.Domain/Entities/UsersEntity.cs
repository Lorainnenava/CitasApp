namespace MyApp.Domain.Entities
{
    public class UsersEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string? SecondName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string IdentificatiónNumber { get; set; } = string.Empty;
        public int IdentificationTypeId { get; set; }
        public int GenderId { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public int RoleId { get; set; }
        public string? CodeValidation { get; set; }
        public bool IsActive { get; set; } = false;
        public string Phone { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        // Relaciones
        public IdentificationTypesEntity IdentificationType { get; set; } = new();
        public GendersEntity Gender { get; set; } = new();
        public RolesEntity Role { get; set; } = new();
    }
}
