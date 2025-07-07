using MyApp.Application.DTOs.ChangeHospitalRequests;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Interfaces.UseCases.ChangeHospitalRequests
{
    public interface IChangeHospitalRequestGetAllPaginatedUseCase
    {
        Task<PaginationResult<ChangeHospitalRequestListResponse>> Execute(int Page, int Size, int HospitalId);
    }
}
