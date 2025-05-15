using MyApp.Application.DTOs.UserSessions;

namespace MyApp.Application.Interfaces.UseCases.UserSessions
{
    public interface IUserSessionsCreateUseCase
    {
        Task<UserSessionResponseDto> Execute(UserSessionRequestDto request);
    }
}
