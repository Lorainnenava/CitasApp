using Application.DTOs.User;
using Application.Interfaces.UseCases;
using Application.UseCases.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
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

        // Inyección del caso de uso
        public UserController(IUserCreateUseCase userCreateUseCase, IUserGetByIdUseCase userGetByIdUseCase, IUserGetAllUseCase userGetAllUseCase, IUserUpdateUseCase userUpdateUseCase,
            IUserDeleteUseCase userDeleteUseCase)
        {
            _userCreateUseCase = userCreateUseCase;
            _userGetByIdUseCase = userGetByIdUseCase;
            _userGetAllUseCase = userGetAllUseCase;
            _userUpdateUseCase = userUpdateUseCase;
            _userDeleteUseCase = userDeleteUseCase;
        }

        // POST: api/user/create
        /// <summary>
        /// Crear un usuario.
        /// </summary>
        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
        {
            var response = await _userCreateUseCase.Execute(request);

            return CreatedAtAction(nameof(CreateUser), new { id = response.Id }, response);
        }

        // GET: api/user/getById
        /// <summary>
        /// Busca un usuario por su Id.
        /// </summary>
        [HttpGet("getById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            var response = await _userGetByIdUseCase.Execute(id);

            if (response == null)
                return NotFound(new { Message = "User not found" });

            return Ok(response);
        }


        // GET: api/users/getAll
        /// <summary>
        /// Busca todos los usuarios.
        /// </summary>
        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers()
        {
            var users = await _userGetAllUseCase.Execute();

            return Ok(users);
        }

        /// <summary>
        /// Actualizar un usuario.
        /// </summary>
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequest request)
        {
            var response = await _userUpdateUseCase.Execute(id, request);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Eliminar un usuario.
        /// </summary>
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
