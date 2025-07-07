using MyApp.Application.DTOs.ChangeHospitalRequests;

namespace MyApp.Application.Interfaces.UseCases.ChangeHospitalRequests
{
    public interface IChangeHospitalRequestChangeStateUseCase
    {
        Task<ChangeHospitalRequestResponse> Execute(ChangeHospitalRequestChangeStateRequest request);
    }
}
