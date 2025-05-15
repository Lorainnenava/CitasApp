using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.DTOs.Users
{
    public class UserRequest
    {
        [Required(ErrorMessage = "El UserName es obligatorio.")]
        public string UserName { get; set; }= string.Empty;

        [Required(ErrorMessage = "El Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Password es obligatorio.")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "La dirección debe tener al menos 4 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "El FirstName es obligatorio.")]
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El LastName es obligatorio.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El IdentificatiónNumber es obligatorio.")]
        public string IdentificatiónNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "El TypeIdentification es obligatorio.")]
        public int TypeIdentification { get; set; }

        [Required(ErrorMessage = "El Phone es obligatorio.")]
        [StringLength(10, ErrorMessage = "El telefono no debe tener más de 10 caracteres.")]
        public string Phone { get; set; } = string.Empty;
    }
}
