using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Utilitaries.BloodTypes
{
    public class BloodTypeRequest
    {
        [Required]
        public int BloodTypeId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
