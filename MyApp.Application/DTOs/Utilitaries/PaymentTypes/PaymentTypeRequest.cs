using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Utilitaries.PaymentTypes
{
    public class PaymentTypeRequest
    {
        [Required]
        public int PaymentTypeId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
