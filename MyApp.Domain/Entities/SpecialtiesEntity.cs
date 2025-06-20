using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class SpecialtiesEntity
    {
        [Key]
        public int SpecialtyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; } = true;
    }
}
