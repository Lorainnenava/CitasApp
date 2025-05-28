using MyApp.Application.DTOs.HospitalSchedules;

namespace MyApp.Application.Interfaces.UseCases.HospitalSchedules
{
    public interface IHospitalUpdateUseCase
    {
        Task<HospitalScheduleResponse> Execute(int HospitalScheduleId, HospitalScheduleRequest request);
    }
}
