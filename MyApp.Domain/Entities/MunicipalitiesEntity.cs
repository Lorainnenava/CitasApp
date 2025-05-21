namespace MyApp.Domain.Entities
{
    public class MunicipalitiesEntity
    {
        public int MunicipalityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool? IsSystemDefined { get; set; } = false;
    }
}
