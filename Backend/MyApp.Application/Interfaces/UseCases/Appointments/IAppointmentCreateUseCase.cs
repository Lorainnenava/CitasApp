using MyApp.Application.DTOs.Appointments;

namespace MyApp.Application.Interfaces.UseCases.Appointments
{
    public interface IAppointmentCreateUseCase
    {
        Task<AppointmentResponse> Execute(AppointmentRequest request);
    }
}
