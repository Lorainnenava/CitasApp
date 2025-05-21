namespace MyApp.Domain.Entities
{
    public class GendersEntity
    {
        public int GenderId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool? IsSystemDefined { get; set; } = false;
    }
}
