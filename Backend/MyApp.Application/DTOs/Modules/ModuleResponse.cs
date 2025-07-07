using MyApp.Application.DTOs.SubModules;

namespace MyApp.Application.DTOs.Modules
{
    public class ModuleResponse
    {
        public int ModuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<SubModuleResponse> SubModules { get; set; } = [];
    }
}
