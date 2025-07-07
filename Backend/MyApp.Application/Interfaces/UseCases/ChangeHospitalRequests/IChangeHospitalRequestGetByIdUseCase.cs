using MyApp.Application.DTOs.ChangeHospitalRequests;

namespace MyApp.Application.Interfaces.UseCases.ChangeHospitalRequests
{
    public interface IChangeHospitalRequestGetByIdUseCase
    {
        Task<ChangeHospitalRequestResponse> Execute(int ChangeHospitalRequestId);
    }
}
