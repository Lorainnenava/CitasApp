namespace MyApp.Domain.Entities
{
    public class StatusTypesEntity
    {
        public int StatusTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool? IsSystemDefined { get; set; } = false;
    }
}
