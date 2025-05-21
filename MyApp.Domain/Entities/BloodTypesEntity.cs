namespace MyApp.Domain.Entities
{
    public class BloodTypesEntity
    {
        public int BloodTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool? IsSystemDefined { get; set; } = false;
    }
}
