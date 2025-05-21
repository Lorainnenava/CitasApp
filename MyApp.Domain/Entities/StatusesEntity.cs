namespace MyApp.Domain.Entities
{
    public class StatusesEntity
    {
        public int StatusId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StatusTypeId { get; set; }
        public bool? IsSystemDefined { get; set; } = false;

        // Relaciones
        public StatusTypesEntity StatusType { get; set; } = new();
    }
}
