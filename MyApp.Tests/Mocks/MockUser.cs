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
                    HospitalId = 1,
                    FirstName = "Usuario",
                    MiddleName = "Prueba",
                    LastName = "Ejemplo",
                    SecondName = null,
                    Email = "usuario.prueba@example.com",
                    PasswordHash = "hashed_password_placeholder",
                    IdentificationNumber = "1234567890",
                    IdentificationTypeId = 1,
                    GenderId = 1,
                    DateOfBirth = new DateTime(2004, 7, 23),
                    RoleId = 1,
                    CodeValidation = null,
                    IsActive = true,
                    Phone = "+57 300 123 4567",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Hospital = new HospitalsEntity
                    {
                        Name = "Hospital Central"
                    },
                    Gender = new GendersEntity
                    {
                        Name = "Masculino"
                    },
                    Role = new RolesEntity
                    {
                        Name = "Admin"
                    },
                    IdentificationType = new IdentificationTypesEntity
                    {
                        Name = "Cédula de Ciudadanía"
                    }
                },
                new UsersEntity
                {
                    UserId = 2,
                    HospitalId=1,
                    FirstName = "Usuario2",
                    MiddleName = "Prueba2",
                    LastName = "Ejemplo2",
                    SecondName = null,
                    Email = "usuario2.prueba@example.com",
                    PasswordHash = "hashed_password_placeholder",
                    IdentificationNumber = "23456789",
                    IdentificationTypeId = 1,
                    GenderId = 1,
                    DateOfBirth = new DateTime(2004, 7, 23),
                    RoleId = 1,
                    CodeValidation = null,
                    IsActive = true,
                    Phone = "+57 300 123 4567",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Hospital = new HospitalsEntity
                    {
                        Name = "Hospital Central"
                    },
                    Gender = new GendersEntity
                    {
                        Name = "Masculino"
                    },
                    Role = new RolesEntity
                    {
                        Name = "Admin"
                    },
                    IdentificationType = new IdentificationTypesEntity
                    {
                        Name = "Cédula de Ciudadanía"
                    }
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
                IdentificationNumber = "1234567890",
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

        public static UsersEntity MockOneUserEntityWithCodeValidation()
        {
            return new UsersEntity
            {
                UserId = 1,
                HospitalId = 1,
                FirstName = "Usuario",
                MiddleName = "Prueba",
                LastName = "Ejemplo",
                SecondName = null,
                Email = "usuario.prueba@example.com",
                PasswordHash = "hashed_password_placeholder",
                IdentificationNumber = "1234567890",
                IdentificationTypeId = 1,
                GenderId = 1,
                DateOfBirth = new DateTime(2004, 7, 23),
                RoleId = 2,
                CodeValidation = "56412",
                IsActive = false,
                Phone = "+57 300 123 4567",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public static UsersEntity MockOneUserEntityToCreate()
        {
            return new UsersEntity
            {
                UserId = 3,
                HospitalId = 1,
                FirstName = "Usuario",
                MiddleName = "Prueba",
                LastName = "Ejemplo",
                SecondName = null,
                Email = "usuario.prueba@example.com",
                PasswordHash = "hashed_password_placeholder",
                IdentificationNumber = "1234567890",
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
                HospitalId = 1,
                MiddleName = "Prueba",
                LastName = "Ejemplo",
                SecondName = null,
                Password = "123456",
                Email = "usuario.prueba@example.com",
                IdentificationNumber = "1234567890",
                IdentificationTypeId = 1,
                GenderId = 1,
                DateOfBirth = new DateTime(2004, 7, 23),
                RoleId = 2,
                Phone = "3001234567",
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
                IdentificationNumber = "1234567890",
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

        public static UserUpdateRequest MockOneUserEntityToUpdateRequest()
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
                Phone = "3001234685",
            };
        }

        public static UserCreateRequest MockUserCreateInvalidCredentials()
        {
            return new UserCreateRequest
            {
                Email = "",
                Password = "123"
            };
        }

        public static UserCodeValidationRequest MockUserValidateInvalidCredentials()
        {
            return new UserCodeValidationRequest
            {
                Email = "",
                CodeValidation = "123"
            };
        }

        public static UserCodeValidationRequest MockUserValidateRequest()
        {
            return new UserCodeValidationRequest
            {
                Email = "usuario.prueba@example.com",
                CodeValidation = "56412",
            };
        }

        public static UserChangePasswordRequest UserChangePasswordRequestInvalid()
        {
            return new UserChangePasswordRequest
            {
                CurrentPassword = "123",
                NewPassword = "123"
            };
        }

        public static UserChangePasswordRequest UserChangePasswordRequestValid()
        {
            return new UserChangePasswordRequest
            {
                CurrentPassword = "12345",
                NewPassword = "123456"
            };
        }
    }
}
