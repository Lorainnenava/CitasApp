using FluentValidation;
using MyApp.Application.DTOs.Hospitals;
using MyApp.Application.Validators.HospitalSchedules;

namespace MyApp.Application.Validators.Hospitals
{
    public class HospitalCreateValidator : AbstractValidator<HospitalCreateRequest>
    {
        public HospitalCreateValidator()
        {
            RuleFor(x => x.MunicipalityId)
                .NotEmpty().WithMessage("El MunicipalityId es obligatorio.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El Name es obligatorio.")
                .MaximumLength(100).WithMessage("El Name no puede superar los 100 caracteres.");

            RuleFor(x => x.Nit)
                .NotEmpty().WithMessage("El campo NIT es obligatorio.")
                .Matches(@"^\d{8,10}$").WithMessage("El NIT debe tener entre 8 y 10 dígitos.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("El campo Address es obligatorio.")
                .MaximumLength(200).WithMessage("El campo Address no puede superar los 200 caracteres.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("El número de teléfono es obligatorio.")
                .MaximumLength(10).WithMessage("El número de teléfono debe tener exactamente 10 dígitos.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El correo electrónico debe tener un formato válido.");

            RuleFor(x => x.HospitalSchedule)
                .SetValidator(new HospitalScheduleValidator())
                .WithMessage("El horario del hospital no es válido.");
        }
    }
}
