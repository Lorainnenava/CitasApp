using FluentValidation;
using PortalAdmin.Models;

namespace PortalAdmin.Validators
{
    public class LoginValidator: AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Correo es requerido.")
                .EmailAddress().WithMessage("Correo invalido.");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Contraseña es requerida.")
                .MinimumLength(6).WithMessage("La contaseña debe tener menos de 6 digitos.");
        }
    }
}
