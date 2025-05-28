using FluentValidation;
using MyApp.Application.DTOs.Hospitals;
using MyApp.Application.Validators.HospitalSchedules;

namespace MyApp.Application.Validators.Hospitals
{
    public class HospitalUpdateValidator : AbstractValidator<HospitalUpdateRequest>
    {
        public HospitalUpdateValidator()
        {
            RuleFor(x => x.MunicipalityId)
                .NotEmpty().WithMessage("Municipality ID is required.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Hospital name is required.")
                .MaximumLength(100).WithMessage("Hospital name cannot exceed 100 characters.");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Hospital address is required.")
                .MaximumLength(200).WithMessage("Hospital address cannot exceed 200 characters.");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(10).WithMessage("Phone number must be exactly 10 digits.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");
            RuleFor(x => x.HospitalSchedule)
                .SetValidator(new HospitalScheduleValidator())
                .WithMessage("Hospital schedule is invalid.");
        }
    }
}
