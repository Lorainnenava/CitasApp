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
        public readonly IUserCreateUseCase _userCreateUseCase;
        public readonly IUserGetByIdUseCase _userGetByIdUseCase;
        public readonly IUserGetAllPaginatedUseCase _userGetAllUseCase;
        public readonly IUserUpdateUseCase _userUpdateUseCase;
        public readonly IUserSetActiveStatusUseCase _userDeleteUseCase;
        public readonly IUserChangePasswordUseCase _userChangePasswordUseCase;
        public readonly IUserValidateUseCase _userValidateUseCase;

        public UserController(
            IUserCreateUseCase userCreateUseCase,
            IUserGetByIdUseCase userGetByIdUseCase,
            IUserGetAllPaginatedUseCase userGetAllUseCase,
            IUserUpdateUseCase userUpdateUseCase,
            IUserSetActiveStatusUseCase userDeleteUseCase,
            IUserChangePasswordUseCase userChangePasswordUseCase,
            IUserValidateUseCase userValidateUseCase)
        {
            _userCreateUseCase = userCreateUseCase;
            _userGetByIdUseCase = userGetByIdUseCase;
            _userGetAllUseCase = userGetAllUseCase;
            _userUpdateUseCase = userUpdateUseCase;
            _userDeleteUseCase = userDeleteUseCase;
            _userChangePasswordUseCase = userChangePasswordUseCase;
            _userValidateUseCase = userValidateUseCase;
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest request)
        {
            var result = await _userCreateUseCase.Execute(request);
            return CreatedAtAction(nameof(CreateUser), new { id = result.UserId }, result);
        }

        [HttpGet("getById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            var result = await _userGetByIdUseCase.Execute(id);
            return Ok(result);
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<ActionResult<PaginationResult<UserResponse>>> GetAllUsers(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var result = await _userGetAllUseCase.Execute(page, size);
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateRequest request)
        {
            var result = await _userUpdateUseCase.Execute(id, request);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userDeleteUseCase.Execute(id);
            return Ok(result);
        }
    }
}
