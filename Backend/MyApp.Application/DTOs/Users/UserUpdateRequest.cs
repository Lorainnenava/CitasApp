﻿namespace MyApp.Application.DTOs.Users
{
    public class UserUpdateRequest
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string? SecondName { get; set; }
        public string Email { get; set; } = string.Empty;
        public int IdentificationTypeId { get; set; }
        public int GenderId { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public string Phone { get; set; } = string.Empty;
    }
}
