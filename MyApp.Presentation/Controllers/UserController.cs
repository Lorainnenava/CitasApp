using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs.Users;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Shared.DTOs;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserCreateUseCase _userCreateUseCase;
        public readonly IUserGetByIdUseCase _userGetByIdUseCase;
        public readonly IUserGetAllUseCase _userGetAllUseCase;
        public readonly IUserUpdateUseCase _userUpdateUseCase;
        public readonly IUserDeleteUseCase _userDeleteUseCase;

        public UserController(IUserCreateUseCase userCreateUseCase, IUserGetByIdUseCase userGetByIdUseCase, IUserGetAllUseCase userGetAllUseCase, IUserUpdateUseCase userUpdateUseCase,
            IUserDeleteUseCase userDeleteUseCase)
        {
            _userCreateUseCase = userCreateUseCase;
            _userGetByIdUseCase = userGetByIdUseCase;
            _userGetAllUseCase = userGetAllUseCase;
            _userUpdateUseCase = userUpdateUseCase;
            _userDeleteUseCase = userDeleteUseCase;
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
        {
            var response = await _userCreateUseCase.Execute(request);

            return CreatedAtAction(nameof(CreateUser), new { id = response.UserId }, response);
        }

        [HttpGet("getById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            var response = await _userGetByIdUseCase.Execute(id);

            if (response == null)
                return NotFound(new { Message = "User not found" });

            return Ok(response);
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<ActionResult<PaginationResult<UserResponse>>> GetAllUsers()
        {
            var users = await _userGetAllUseCase.Execute(1, 1);

            return Ok(users);
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequest request)
        {
            var response = await _userUpdateUseCase.Execute(id, request);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userDeleteUseCase.Execute(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
