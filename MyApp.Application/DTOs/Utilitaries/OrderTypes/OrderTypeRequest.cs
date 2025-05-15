using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Utilitaries.OrderTypes
{
    public class OrderTypeRequest
    {
        [Required]
        public int OrderTypeId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
