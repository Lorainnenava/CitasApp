using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Utilitaries.Genders
{
    public class GenderRequest
    {
        [Required]
        public int GenderId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
