using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs.SubModulePermissions;
using MyApp.Application.Interfaces.UseCases.SubModulePermissions;

namespace MyApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubModulePermissionController : ControllerBase
    {
        public readonly IAssignPermissionsToSubModuleUseCase _assignPermissionsToSubModuleUseCase;
        public readonly IUpdatePermissionsFromSubModuleUseCase _updatePermissionsFromSubModuleUseCase;
        public readonly IGetSubModulePermissionsBySubModuleIdUseCase _getSubModulePermissionsBySubModuleIdUseCase;

        public SubModulePermissionController(
            IAssignPermissionsToSubModuleUseCase assignPermissionsToSubModuleUseCase,
            IUpdatePermissionsFromSubModuleUseCase updatePermissionsFromSubModuleUseCase,
            IGetSubModulePermissionsBySubModuleIdUseCase getSubModulePermissionsBySubModuleIdUseCase
            )
        {
            _assignPermissionsToSubModuleUseCase = assignPermissionsToSubModuleUseCase;
            _updatePermissionsFromSubModuleUseCase = updatePermissionsFromSubModuleUseCase;
            _getSubModulePermissionsBySubModuleIdUseCase = getSubModulePermissionsBySubModuleIdUseCase;
        }

        [HttpPost("assignPermissions")]
        [AllowAnonymous]
        public async Task<IActionResult> AssignPermissions([FromBody] SubModulePermissionAssignToSubModuleRequest request)
        {
            var result = await _assignPermissionsToSubModuleUseCase.Execute(request);
            return Ok(result);
        }

        [HttpGet("{id}/permissions")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSubModulePermissionBySubModuleId(int id)
        {
            var result = await _getSubModulePermissionsBySubModuleIdUseCase.Execute(id);
            return Ok(result);
        }

        [HttpPut("{id}/permissions")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateSubModulePermissionsBySubModuleId(int id, [FromBody] SubModulePermissionUpdateRequest request)
        {
            var result = await _updatePermissionsFromSubModuleUseCase.Execute(id, request);
            return Ok(result);
        }
    }
}
