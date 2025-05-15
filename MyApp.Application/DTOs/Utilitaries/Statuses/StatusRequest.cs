using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Utilitaries.Statuses
{
    public class StatusRequest
    {
        [Required]
        public int StatusId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
