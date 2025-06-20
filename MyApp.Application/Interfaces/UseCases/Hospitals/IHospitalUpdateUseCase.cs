using MyApp.Application.DTOs.Hospitals;

namespace MyApp.Application.Interfaces.UseCases.Hospitals
{
    public interface IHospitalUpdateUseCase
    {
        Task<HospitalResponse> Execute(int HospitalId, HospitalUpdateRequest request);
    }
}
