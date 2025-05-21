namespace MyApp.Domain.Entities
{
    public class SpecialtiesEntity
    {
        public int SpecialtyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool? IsSystemDefined { get; set; } = false;
    }
}
