using MyApp.Application.DTOs.HospitalSchedules;

namespace MyApp.Application.Interfaces.UseCases.HospitalSchedules
{
    public interface IHospitalScheduleGetUseCase
    {
        Task<HospitalScheduleResponse> Execute();
    }
}
