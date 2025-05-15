using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Utilitaries.Diseases
{
    public class DiseaseRequest
    {
        [Required]
        public int DiseaseId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
