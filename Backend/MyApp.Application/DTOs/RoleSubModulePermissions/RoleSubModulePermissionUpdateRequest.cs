using MyApp.Application.DTOs.SubModulePermissions;

namespace MyApp.Application.DTOs.RoleSubModulePermissions
{
    public class RoleSubModulePermissionUpdateRequest
    {
        public List<RoleSubModulePermissionDto> ActiveRoleSubModulePermissionIds { get; set; } = [];
        public List<SubModulePermissionDto> NewRoleSubModulePermissionIdsToAssign { get; set; } = [];
    }
}
