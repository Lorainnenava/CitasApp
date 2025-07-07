using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class OrderTypesEntity
    {
        [Key]
        public int OrderTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; } = true;
    }
}
