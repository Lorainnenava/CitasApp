using MyApp.Application.DTOs.Permissions;

namespace MyApp.Application.DTOs.SubModulePermissions
{
    public class SubModulePermissionAssignToSubModuleRequest
    {
        public int SubModuleId { get; set; }
        public List<PermissionDto> Permissions { get; set; } = [];
    }
}
