using MyApp.Application.DTOs.Permissions;

namespace MyApp.Application.DTOs.SubModulePermissions
{
    public class SubModulePermissionUpdateRequest
    {
        public List<SubModulePermissionDto> ActivePermissionIds { get; set; } = [];
        public List<PermissionDto> NewPermissionIdsToAssign { get; set; } = [];
    }
}
