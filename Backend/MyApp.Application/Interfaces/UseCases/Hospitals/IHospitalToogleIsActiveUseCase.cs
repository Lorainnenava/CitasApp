using MyApp.Application.DTOs.Hospitals;

namespace MyApp.Application.Interfaces.UseCases.Hospitals
{
    public interface IHospitalToogleIsActiveUseCase
    {
        Task<HospitalResponse> Execute(int HospitalId);
    }
}
