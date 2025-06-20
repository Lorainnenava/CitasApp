using MyApp.Application.DTOs.Hospitals;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Interfaces.UseCases.Hospitals
{
    public interface IHospitalGetAllPaginatedUseCase
    {
        Task<PaginationResult<HospitalListResponse>> Execute(int Page, int Size);
    }
}
