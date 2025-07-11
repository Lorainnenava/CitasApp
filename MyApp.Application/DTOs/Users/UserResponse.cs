﻿namespace MyApp.Application.DTOs.Users
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdentificatiónNumber { get; set; } = string.Empty;
        public int TypeIdentification { get; set; }
        public string Phone { get; set; } = string.Empty;
    }
}
