using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs.Roles;
using MyApp.Application.Interfaces.UseCases.Common;
using MyApp.Domain.Entities;

namespace MyApp.Presentation.Controllers
{
    [Route("")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public readonly IGenericGetAllUseCase<RolesEntity, RoleResponse> _roleGetAll;

        public RoleController(IGenericGetAllUseCase<RolesEntity, RoleResponse> roleGetAll)
        {
            _roleGetAll = roleGetAll;
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleGetAll.Execute();
            return Ok(result);
        }
    }
}
