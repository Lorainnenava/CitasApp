using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Utilitaries.Municipalities
{
    public class MunicipalityRequest
    {
        [Required]
        public int MunicipalityId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
