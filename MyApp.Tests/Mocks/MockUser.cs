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
                    UserName = "Prueba123",
                    FirstName = "Carlos",
                    SecondName = "Andrés",
                    LastName = "Pérez",
                    Email = "carlos.perez@example.com",
                    Password = "1234",
                    IdentificatiónNumber = "123456789",
                    Phone = "3001234567",
                    TypeIdentification = 1
                },
                new UsersEntity
                {
                    UserId = 2,
                    UserName = "DevJane",
                    FirstName = "Jane",
                    SecondName = "Marie",
                    LastName = "Doe",
                    Email = "jane.doe@example.com",
                    Password = "secure123",
                    IdentificatiónNumber = "987654321",
                    Phone = "3107654321",
                    TypeIdentification = 2
                }
            ];
        }

        public static UsersEntity MockOneUserEntity()
        {
            return new UsersEntity
            {
                UserId = 1,
                FirstName = "userPrueba",
                MiddleName = "Del Carmen",
                LastName = "userPrueba",
                SecondName = "Test",
                Email = "userprueba@example.com",
                PasswordHash = "hashed_password_123",
                IdentificatiónNumber = "1234567890",
                IdentificationTypeId = 1,
                GenderId = 1,
                DateOfBirth = new DateTime(2004, 7, 23),
                RoleId = 2,
                CodeValidation = "VAL123",
                IsActive = true,
                Phone = "+57 3011234567",
                CreatedAt = DateTime.Now.AddMonths(-2),
                UpdatedAt = DateTime.Now
            };
        }

        public static UsersEntity MockOneUserEntityToCreate()
        {
            return new UsersEntity
            {
                FirstName = "userPrueba",
                MiddleName = "Del Carmen",
                LastName = "userPrueba",
                SecondName = "Test",
                Email = "userprueba@example.com",
                PasswordHash = "hashed_password_123",
                IdentificatiónNumber = "1234567890",
                IdentificationTypeId = 1,
                GenderId = 1,
                DateOfBirth = new DateTime(2004, 7, 23),
                RoleId = 2,
                CodeValidation = "VAL123",
                IsActive = true,
                Phone = "+57 3011234567",
                CreatedAt = DateTime.Now.AddMonths(-2),
            };
        }

        public static UserRequest MockOneUserRequest()
        {
            return new UserRequest
            {
                UserName = "NuevoUsuario",
                FirstName = "Luis",
                SecondName = "Miguel",
                LastName = "Gómez",
                Email = "luis@example.com",
                Password = "pass123",
                IdentificatiónNumber = "555555555",
                Phone = "3200000000",
                TypeIdentification = 3
            };
        }

        public static UserResponse MockOneUserResponse()
        {
            return new UserResponse
            {
                UserName = "NuevoUsuario",
                FirstName = "Luis",
                SecondName = "Miguel",
                LastName = "Gómez",
                Email = "luis@example.com",
                IdentificatiónNumber = "555555555",
                Phone = "3200000000",
                TypeIdentification = 3
            };
        }

        public static UsersEntity MockOneUserEntityUpdated()
        {
            return new UsersEntity
            {
                UserId = 2,
                UserName = "DevJane",
                FirstName = "Jane",
                SecondName = "Marie",
                LastName = "Doe",
                Email = "jane.doe567@example.com",
                Password = "secure123",
                IdentificatiónNumber = "987654321",
                Phone = "3107654321",
                TypeIdentification = 2
            };
        }

        public static UserRequest MockOneUserEntityToUpdate()
        {
            return new UserRequest
            {
                UserName = "DevJane",
                FirstName = "Jane",
                SecondName = "Marie",
                LastName = "Doe",
                Email = "jane.doe567@example.com",
                Password = "secure123",
                IdentificatiónNumber = "987654321",
                Phone = "3107654321",
                TypeIdentification = 2
            };
        }
    }
}
