using MyApp.Application.DTOs.Users;

namespace MyApp.Application.Interfaces.UseCases.Users
{
    public interface IUserValidateUseCase
    {
        Task<bool> Execute(UserCodeValidationRequest request);
    }
}
