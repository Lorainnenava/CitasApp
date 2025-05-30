namespace MyApp.Domain.Entities
{
    public class SubModulesEntity
    {
        public int SubModuleId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public int Order { get; set; }

        // Relaciones
        public ModulesEntity Module { get; set; } = null!;
    }
}
