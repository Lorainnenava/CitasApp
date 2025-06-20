using MyApp.Application.DTOs.Hospitals;

namespace MyApp.Application.Interfaces.UseCases.Hospitals
{
    public interface IHospitalGetByIdUseCase
    {
        Task<HospitalResponse> Execute(int HospitalId);
    }
}
