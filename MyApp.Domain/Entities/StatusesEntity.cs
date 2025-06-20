using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class StatusesEntity
    {
        [Key]
        public int StatusId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StatusTypeId { get; set; }
        public bool IsSystemDefined { get; set; } = true;

        // Relaciones
        public StatusTypesEntity StatusType { get; set; } = null!;
    }
}
