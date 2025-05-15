using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Utilitaries.IdentificationTypes
{
    public class IdentificationTypeRequest
    {
        [Required]
        public int IdentificationTypeId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
