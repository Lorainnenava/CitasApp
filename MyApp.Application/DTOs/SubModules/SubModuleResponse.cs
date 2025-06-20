using MyApp.Application.DTOs.SubModulePermissions;

namespace MyApp.Application.DTOs.SubModules
{
    public class SubModuleResponse
    {
        public int SubModuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<SubModulePermissionResponse> SubModulePermissions { get; set; } = [];
    }
}
