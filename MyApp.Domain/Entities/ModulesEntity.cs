using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class ModulesEntity
    {
        [Key]
        public int ModuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public int Order { get; set; }
        public bool IsActive { get; set; } = true;

        // Relaciones
        public ICollection<SubModulesEntity> SubModules { get; set; } = [];
    }
}
