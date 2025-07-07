using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class DiseasesEntity
    {
        [Key]
        public int DiseaseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; } = true;
    }
}
