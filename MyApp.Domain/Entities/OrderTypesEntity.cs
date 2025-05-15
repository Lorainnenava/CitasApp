using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class OrderTypesEntity
    {
        [Key]
        public int OrderTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        // Relaciones
        public ICollection<MedicalOrdersEntity> MedicalOrders { get; set; } = [];
    }
}
