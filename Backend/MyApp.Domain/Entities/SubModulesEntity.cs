using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    public class SubModulesEntity
    {
        [Key]
        public int SubModuleId { get; set; }
        [ForeignKey("Module")]
        public int ModuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public int Order { get; set; }
        public bool IsActive { get; set; } = true;

        // Relaciones
        public ModulesEntity Module { get; set; } = null!;
        public ICollection<SubmodulePermissionsEntity> SubModulePermissions { get; set; } = [];
    }
}
