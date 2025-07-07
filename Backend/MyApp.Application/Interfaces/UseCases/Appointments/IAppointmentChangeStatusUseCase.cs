using MyApp.Application.DTOs.Appointments;

namespace MyApp.Application.Interfaces.UseCases.Appointments
{
    public interface IAppointmentChangeStatusUseCase
    {
        Task<AppointmentResponse> Execute(int StatusId, int AppointmentId);
    }
}
