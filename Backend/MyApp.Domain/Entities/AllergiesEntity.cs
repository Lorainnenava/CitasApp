using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class AllergiesEntity
    {
        [Key]
        public int AllergyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; } = true;
    }
}
