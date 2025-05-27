using FluentValidation;
using MyApp.Application.DTOs.ChangeHospitalRequests;

namespace MyApp.Application.Validators.ChangeHospitalRequest
{
    public class ChangeHospitalRequestCreateValidator : AbstractValidator<ChangeHospitalRequestCreateRequest>
    {
        public ChangeHospitalRequestCreateValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");
            RuleFor(x => x.CurrentHospitalId)
                .NotEmpty().WithMessage("CurrentHospitalId is required.")
                .GreaterThan(0).WithMessage("CurrentHospitalId must be greater than 0.");
            RuleFor(x => x.NewHospitalId)
                .NotEmpty().WithMessage("NewHospitalId is required.")
                .GreaterThan(0).WithMessage("NewHospitalId must be greater than 0.");
            RuleFor(x => x.ReasonForChange)
                .NotEmpty().WithMessage("ReasonForChange is required.")
                .MaximumLength(500).WithMessage("ReasonForChange must not exceed 500 characters.");
        }
    }
}
