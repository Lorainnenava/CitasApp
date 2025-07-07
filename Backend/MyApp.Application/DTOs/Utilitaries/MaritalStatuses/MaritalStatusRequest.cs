using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Utilitaries.MaritalStatuses
{
    public class MaritalStatusRequest
    {
        [Required]
        public int MaritalStatusId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
