using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs.Permissions;
using MyApp.Application.Interfaces.UseCases.Common;
using MyApp.Domain.Entities;

namespace MyApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        public readonly IGenericGetAllUseCase<PermissionsEntity, PermissionResponse> _permissionGetAll;

        public PermissionController(IGenericGetAllUseCase<PermissionsEntity, PermissionResponse> permissionGetAll)
        {
            _permissionGetAll = permissionGetAll;
        }

        [HttpGet("getAll")]
        [Authorize]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _permissionGetAll.Execute();
            return Ok(result);
        }
    }
}
