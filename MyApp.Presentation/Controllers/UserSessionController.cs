using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs.UserSessions;
using MyApp.Application.Interfaces.UseCases.UserSessions;

namespace MyApp.Presentation.Controllers
{
    [Route("")]
    [ApiController]
    public class UserSessionController : ControllerBase
    {
        public readonly IUserSessionsCreateUseCase _userSessionsCreateUseCase;
        public readonly IUserSessionRevokedUseCase _userSessionRevokedUseCase;

        public UserSessionController(
            IUserSessionsCreateUseCase userSessionsCreateUseCase,
            IUserSessionRevokedUseCase userSessionRevokedUseCase)
        {
            _userSessionsCreateUseCase = userSessionsCreateUseCase;
            _userSessionRevokedUseCase = userSessionRevokedUseCase;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserSessionRequest request)
        {
            var result = await _userSessionsCreateUseCase.Execute(request);
            return Ok(result);
        }

        [HttpGet("logout/{refreshToken}")]
        [Authorize]
        public async Task<IActionResult> GetByIdUser(string refreshToken)
        {
            var result = await _userSessionRevokedUseCase.Execute(refreshToken);
            return Ok(result);
        }
    }
}
