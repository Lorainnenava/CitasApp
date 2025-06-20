using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class MunicipalitiesEntity
    {
        [Key]
        public int MunicipalityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; } = true;
    }
}
