using MyApp.Application.DTOs.ChangeHospitalRequests;

namespace MyApp.Application.Interfaces.UseCases.ChangeHospitalRequests
{
    public interface IChangeHospitalRequestCreateUseCase
    {
        Task<ChangeHospitalRequestResponse> Execute(ChangeHospitalRequestCreateRequest request);
    }
}
