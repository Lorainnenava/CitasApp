using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Entities
{
    public class PaymentTypesEntity
    {
        [Key]
        public int PaymentTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; } = false;
    }
}
