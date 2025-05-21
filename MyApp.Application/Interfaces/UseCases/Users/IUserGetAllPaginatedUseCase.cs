using MyApp.Application.DTOs.Users;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Interfaces.UseCases.Users
{
    public interface IUserGetAllPaginatedUseCase
    {
        Task<PaginationResult<UserResponse>> Execute(int Page, int Size);
    }
}
