using MyApp.Application.DTOs.Hospitals;

namespace MyApp.Application.Interfaces.UseCases.Hospitals
{
    public interface IHospitalCreateUseCase
    {
        Task<HospitalResponse> Execute(HospitalCreateRequest request);
    }
}
