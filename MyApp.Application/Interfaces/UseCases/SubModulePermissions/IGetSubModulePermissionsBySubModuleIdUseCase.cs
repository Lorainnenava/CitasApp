using MyApp.Application.DTOs.SubModulePermissions;

namespace MyApp.Application.Interfaces.UseCases.SubModulePermissions
{
    public interface IGetSubModulePermissionsBySubModuleIdUseCase
    {
        Task<List<SubModulePermissionGetResponse>> Execute(int SubModuleId);
    }
}
