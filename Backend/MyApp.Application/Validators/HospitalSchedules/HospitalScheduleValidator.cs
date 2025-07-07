using FluentValidation;
using MyApp.Application.DTOs.HospitalSchedules;

namespace MyApp.Application.Validators.HospitalSchedules
{
    public class HospitalScheduleValidator : AbstractValidator<HospitalScheduleRequest>
    {
        public HospitalScheduleValidator()
        {
            RuleFor(x => x.AppointmentDurationMinutes)
                .GreaterThan(0).WithMessage("La duración debe ser mayor a 0 minutos.");

            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime)
                .WithMessage("La hora de inicio debe ser menor que la hora de fin.");

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime)
                .WithMessage("La hora de fin debe ser mayor que la hora de inicio.");

            RuleFor(x => x)
                .Must(HaveAtLeastOneDaySelected)
                .WithMessage("Debe seleccionar al menos un día o activar 'Todos los días'.");
        }

        private bool HaveAtLeastOneDaySelected(HospitalScheduleRequest request)
        {
            if (request.AllDays == true) return true;

            return request.Monday == true ||
                   request.Tuesday == true ||
                   request.Wednesday == true ||
                   request.Thursday == true ||
                   request.Friday == true ||
                   request.Saturday == true;
        }
    }
}
