namespace MyApp.Domain.Entities
{
    public class OrderTypesEntity
    {
        public int OrderTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool? IsSystemDefined { get; set; } = false;
    }
}
