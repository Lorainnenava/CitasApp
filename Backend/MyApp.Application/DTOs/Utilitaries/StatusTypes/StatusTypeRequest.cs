using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Utilitaries.StatusTypes
{
    public class StatusTypeRequest
    {
        [Required]
        public int StatusTypeId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
