using MyApp.Application.DTOs.Users;
using MyApp.Domain.Entities;

namespace MyApp.Tests.Mocks
{
    public class MockUser
    {
        public static List<UsersEntity> MockListUsersEntity()
        {
            return
            [
                new UsersEntity
                {
                    UserId = 1,
                    FirstName = "Usuario",
                    MiddleName = "Prueba",
                    LastName = "Ejemplo",
                    SecondName = null,
                    Email = "usuario.prueba@example.com",
                    PasswordHash = "hashed_password_placeholder",
                    IdentificatiónNumber = "1234567890",
                    IdentificationTypeId = 1,
                    GenderId = 1,
                    DateOfBirth = new DateTime(2004, 7, 23),
                    RoleId = 2,
                    CodeValidation = null,
                    IsActive = true,
                    Phone = "+57 300 123 4567",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new UsersEntity
                {
                    UserId = 2,
                    FirstName = "Usuario2",
                    MiddleName = "Prueba2",
                    LastName = "Ejemplo2",
                    SecondName = null,
                    Email = "usuario2.prueba@example.com",
                    PasswordHash = "hashed_password_placeholder",
                    IdentificatiónNumber = "23456789",
                    IdentificationTypeId = 1,
                    GenderId = 1,
                    DateOfBirth = new DateTime(2004, 7, 23),
                    RoleId = 2,
                    CodeValidation = null,
                    IsActive = true,
                    Phone = "+57 300 123 4567",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            ];
        }

        public static UsersEntity MockOneUserEntity()
        {
            return new UsersEntity
            {
                UserId = 1,
                FirstName = "Usuario",
                MiddleName = "Prueba",
                LastName = "Ejemplo",
                SecondName = null,
                Email = "usuario.prueba@example.com",
                PasswordHash = "hashed_password_placeholder",
                IdentificatiónNumber = "1234567890",
                IdentificationTypeId = 1,
                GenderId = 1,
                DateOfBirth = new DateTime(2004, 7, 23),
                RoleId = 2,
                CodeValidation = null,
                IsActive = true,
                Phone = "+57 300 123 4567",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public static UsersEntity MockOneUserEntityToCreate()
        {
            return new UsersEntity
            {
                UserId = 1,
                FirstName = "Usuario",
                MiddleName = "Prueba",
                LastName = "Ejemplo",
                SecondName = null,
                Email = "usuario.prueba@example.com",
                PasswordHash = "hashed_password_placeholder",
                IdentificatiónNumber = "1234567890",
                IdentificationTypeId = 1,
                GenderId = 1,
                DateOfBirth = new DateTime(2004, 7, 23),
                RoleId = 2,
                CodeValidation = null,
                IsActive = true,
                Phone = "+57 300 123 4567",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public static UserCreateRequest MockOneUserRequest()
        {
            return new UserCreateRequest
            {
                FirstName = "Usuario",
                MiddleName = "Prueba",
                LastName = "Ejemplo",
                SecondName = null,
                Email = "usuario.prueba@example.com",
                IdentificatiónNumber = "1234567890",
                IdentificationTypeId = 1,
                GenderId = 1,
                DateOfBirth = new DateTime(2004, 7, 23),
                RoleId = 2,
                Phone = "+57 300 123 4567",
            };
        }

        public static UserResponse MockOneUserResponse()
        {
            return new UserResponse
            {
                UserId = 1,
                FirstName = "Usuario",
                MiddleName = "Prueba",
                LastName = "Ejemplo",
                SecondName = null,
                Email = "usuario.prueba@example.com",
                IdentificationTypeId = 1,
                GenderId = 1,
                DateOfBirth = new DateTime(2004, 7, 23),
                RoleId = 2,
                Phone = "+57 300 123 4567"
            };
        }

        public static UsersEntity MockOneUserEntityUpdated()
        {
            return new UsersEntity
            {
                UserId = 1,
                FirstName = "Usuario",
                MiddleName = "Prueba",
                LastName = "Ejemplo",
                SecondName = "Segundo apellido",
                Email = "usuario.prueba@example.com",
                PasswordHash = "hashed_password_placeholder",
                IdentificatiónNumber = "1234567890",
                IdentificationTypeId = 1,
                GenderId = 1,
                DateOfBirth = new DateTime(2004, 7, 23),
                RoleId = 2,
                CodeValidation = null,
                IsActive = true,
                Phone = "+57 300 123 4685",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public static UserUpdateRequest MockOneUserEntityToUpdate()
        {
            return new UserUpdateRequest
            {
                FirstName = "Usuario",
                MiddleName = "Prueba",
                LastName = "Ejemplo",
                SecondName = "Segundo apellido",
                Email = "usuario.prueba@example.com",
                IdentificationTypeId = 1,
                GenderId = 1,
                DateOfBirth = new DateTime(2004, 7, 23),
                RoleId = 2,
                Phone = "+57 300 123 4685",
            };
        }
    }
}
