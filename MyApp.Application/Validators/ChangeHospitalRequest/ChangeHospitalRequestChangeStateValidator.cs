using FluentValidation;
using MyApp.Application.DTOs.ChangeHospitalRequests;

namespace MyApp.Application.Validators.ChangeHospitalRequest
{
    public class ChangeHospitalRequestChangeStateValidator : AbstractValidator<ChangeHospitalRequestChangeStateRequest>
    {
        public ChangeHospitalRequestChangeStateValidator()
        {
            RuleFor(x => x.ChangeHospitalRequestId)
                .NotEmpty().WithMessage("ChangeHospitalRequestId is required.")
                .GreaterThan(0).WithMessage("ChangeHospitalRequestId must be greater than 0.");
            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage("StatusId is required.")
                .GreaterThan(0).WithMessage("StatusId must be greater than 0.");
        }
    }
}
