using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs.RoleSubModulePermissions;
using MyApp.Application.Interfaces.UseCases.RoleSubModulePermissions;

namespace MyApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleSubModulePermissionController : ControllerBase
    {
        public readonly IGetSubModulePermissionsByRoleIdUseCase _getSubModulePermissionsByRoleIdUseCase;
        public readonly IAssignSubModulePermissionsToRoleUseCase _assignSubModulePermissionsToRoleUseCase;
        public readonly IUpdateRoleSubModulePermissionsFromRoleUseCase _updateRoleSubModulePermissionsFromRoleUseCase;

        public RoleSubModulePermissionController(
            IGetSubModulePermissionsByRoleIdUseCase getSubModulePermissionsByRoleIdUseCase,
            IAssignSubModulePermissionsToRoleUseCase assignSubModulePermissionsToRoleUseCase,
            IUpdateRoleSubModulePermissionsFromRoleUseCase updateRoleSubModulePermissionsFromRoleUseCase
            )
        {
            _getSubModulePermissionsByRoleIdUseCase = getSubModulePermissionsByRoleIdUseCase;
            _assignSubModulePermissionsToRoleUseCase = assignSubModulePermissionsToRoleUseCase;
            _updateRoleSubModulePermissionsFromRoleUseCase = updateRoleSubModulePermissionsFromRoleUseCase;
        }

        [HttpPost("assignPermissions")]
        [AllowAnonymous]
        public async Task<IActionResult> AssignPermissions([FromBody] RoleSubModulePermissionAssignToRoleRequest request)
        {
            var result = await _assignSubModulePermissionsToRoleUseCase.Execute(request);
            return Ok(result);
        }

        [HttpGet("{id}/subModulePermissions")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSubModulePermissionByRoleId(int id)
        {
            var result = await _getSubModulePermissionsByRoleIdUseCase.Execute(id);
            return Ok(result);
        }

        [HttpPut("{id}/subModulePermissions")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateSubModulePermissionsByRoleId(int id, [FromBody] RoleSubModulePermissionUpdateRequest request)
        {
            var result = await _updateRoleSubModulePermissionsFromRoleUseCase.Execute(id, request);
            return Ok(result);
        }
    }
}
