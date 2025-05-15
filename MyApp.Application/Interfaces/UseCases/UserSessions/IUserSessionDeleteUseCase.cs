using MyApp.Application.DTOs.UserSessions;

namespace MyApp.Application.Interfaces.UseCases.UserSessions
{
    public interface IUserSessionDeleteUseCase
    {
        Task<bool> Execute(UserSessionResponseDto request);
    }
}
