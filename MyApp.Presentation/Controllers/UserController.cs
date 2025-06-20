using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs.Users;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Shared.DTOs;

namespace MyApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserUpdateUseCase _userUpdateUseCase;
        public readonly IUserCreateUseCase _userCreateUseCase;
        public readonly IUserGetByIdUseCase _userGetByIdUseCase;
        public readonly IUserValidateUseCase _userValidateUseCase;
        public readonly IUserGetAllPaginatedUseCase _userGetAllUseCase;
        public readonly IUserChangePasswordUseCase _userChangePasswordUseCase;
        public readonly IUserSetActiveStatusUseCase _userSetActiveStatusUseCase;

        public UserController(
            IUserCreateUseCase userCreateUseCase,
            IUserGetByIdUseCase userGetByIdUseCase,
            IUserGetAllPaginatedUseCase userGetAllUseCase,
            IUserUpdateUseCase userUpdateUseCase,
            IUserSetActiveStatusUseCase userSetActiveStatusUseCase,
            IUserChangePasswordUseCase userChangePasswordUseCase,
            IUserValidateUseCase userValidateUseCase)
        {
            _userCreateUseCase = userCreateUseCase;
            _userGetByIdUseCase = userGetByIdUseCase;
            _userGetAllUseCase = userGetAllUseCase;
            _userUpdateUseCase = userUpdateUseCase;
            _userSetActiveStatusUseCase = userSetActiveStatusUseCase;
            _userChangePasswordUseCase = userChangePasswordUseCase;
            _userValidateUseCase = userValidateUseCase;
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest request)
        {
            var result = await _userCreateUseCase.Execute(request);
            return Ok(result);
        }

        [HttpGet("getById/{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetByIdUser(int UserId)
        {
            var result = await _userGetByIdUseCase.Execute(UserId);
            return Ok(result);
        }

        [HttpGet("getAllPaginated/{HospitalId}")]
        [AllowAnonymous]
        public async Task<ActionResult<PaginationResult<UserResponse>>> GetAllUsers(
            int HospitalId,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var result = await _userGetAllUseCase.Execute(page, size, HospitalId);
            return Ok(result);
        }

        [HttpPut("update/{UserId}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int UserId, [FromBody] UserUpdateRequest request)
        {
            var result = await _userUpdateUseCase.Execute(UserId, request);
            return Ok(result);
        }

        [HttpPut("changePassword/{UserId}")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(int UserId, [FromBody] UserChangePasswordRequest request)
        {
            var result = await _userChangePasswordUseCase.Execute(UserId, request);
            return Ok(result);
        }

        [HttpPut("setActiveStatus/{UserId}")]
        [Authorize]
        public async Task<IActionResult> SetActiveStatus(int UserId)
        {
            var result = await _userSetActiveStatusUseCase.Execute(UserId);
            return Ok(result);
        }

        [HttpPut("validate")]
        [Authorize]
        public async Task<IActionResult> ValidateUser([FromBody] UserCodeValidationRequest request)
        {
            var result = await _userValidateUseCase.Execute(request);
            return Ok(result);
        }
    }
}
