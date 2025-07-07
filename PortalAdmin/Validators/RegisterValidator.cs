using FluentValidation;
using PortalAdmin.Models;

namespace PortalAdmin.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("El primer nombre es obligatorio.");

            RuleFor(x => x.HospitalId)
                .GreaterThan(0).WithMessage("Debe seleccionar un hospital válido.");

            RuleFor(x => x.MiddleName)
                .MaximumLength(50).WithMessage("El segundo nombre no puede tener más de 50 caracteres.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El primer apellido es obligatorio.");

            RuleFor(x => x.SecondName)
                .MaximumLength(50).WithMessage("El segundo apellido no puede tener más de 50 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El correo electrónico no tiene un formato válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");

            RuleFor(x => x.IdentificationNumber)
                .NotEmpty().WithMessage("El número de identificación es obligatorio.");

            RuleFor(x => x.IdentificationTypeId)
                .GreaterThan(0).WithMessage("Debe seleccionar un tipo de identificación válido.");

            RuleFor(x => x.GenderId)
                .GreaterThan(0).WithMessage("Debe seleccionar un género válido.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("La fecha de nacimiento debe ser mayor a la de hoy.");

            RuleFor(x => x.RoleId)
                .GreaterThan(0).WithMessage("Debe seleccionar un rol válido.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("El número de teléfono es obligatorio.")
                .Matches(@"^\d{10}$").WithMessage("El número de teléfono debe contener exactamente 10 dígitos.");
        }
    }
}
