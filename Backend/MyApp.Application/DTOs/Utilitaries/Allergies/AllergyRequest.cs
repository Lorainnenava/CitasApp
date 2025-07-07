using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Utilitaries.Allergies
{
    public class AllergyRequest
    {
        [Required]
        public int AllergyId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
