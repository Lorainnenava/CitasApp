namespace MyApp.Domain.Entities
{
    public class StatusesEntity
    {
        public int StatusId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StatusTypeId { get; set; }

        // Relaciones
        public StatusTypesEntity StatusType { get; set; } = new();
    }
}
