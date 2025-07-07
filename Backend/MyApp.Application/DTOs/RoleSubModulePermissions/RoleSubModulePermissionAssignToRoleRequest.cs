using MyApp.Application.DTOs.SubModulePermissions;

namespace MyApp.Application.DTOs.RoleSubModulePermissions
{
    public class RoleSubModulePermissionAssignToRoleRequest
    {
        public int RoleId { get; set; }
        public List<SubModulePermissionDto> SubModulePermissions { get; set; } = [];
    }
}
