using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class MaritalStatusesEntity
    {
        [Key]
        public int MaritalStatusId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; } = true;
    }
}
