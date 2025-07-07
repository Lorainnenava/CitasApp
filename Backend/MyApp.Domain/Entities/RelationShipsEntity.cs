using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class RelationShipsEntity
    {
        [Key]
        public int RelationShipId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; } = true;
    }
}
