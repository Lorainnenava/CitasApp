using MyApp.Application.DTOs.UserSessions;

namespace MyApp.Application.Interfaces.UseCases.RefreshTokens
{
    public interface IRefreshTokenService
    {
        Task<UserSessionResponseDto> Execute(UserSessionResponseDto request);
    }
}
