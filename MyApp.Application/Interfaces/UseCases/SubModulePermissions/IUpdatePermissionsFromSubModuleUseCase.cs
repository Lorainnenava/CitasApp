using MyApp.Application.DTOs.SubModulePermissions;

namespace MyApp.Application.Interfaces.UseCases.SubModulePermissions
{
    public interface IUpdatePermissionsFromSubModuleUseCase
    {
        Task<bool> Execute(int SubModuleId, SubModulePermissionUpdateRequest request);
    }
}
