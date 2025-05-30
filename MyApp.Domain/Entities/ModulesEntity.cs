namespace MyApp.Domain.Entities
{
    public class ModulesEntity
    {
        public int ModuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public int Order { get; set; }

        // Relaciones
        public ICollection<SubModulesEntity> SubModules { get; set; } = new List<SubModulesEntity>();
    }
}
