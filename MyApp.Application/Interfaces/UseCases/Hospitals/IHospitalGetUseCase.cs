using MyApp.Application.DTOs.HospitalSchedules;

namespace MyApp.Application.Interfaces.UseCases.HospitalSchedules
{
    public interface IHospitalScheduleGetByIdUseCase
    {
        Task<HospitalScheduleResponse> Execute(int HospitalId);
    }
}
