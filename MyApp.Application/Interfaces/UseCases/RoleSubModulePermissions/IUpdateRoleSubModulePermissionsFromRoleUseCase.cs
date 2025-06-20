using MyApp.Application.DTOs.RoleSubModulePermissions;

namespace MyApp.Application.Interfaces.UseCases.RoleSubModulePermissions
{
    public interface IUpdateRoleSubModulePermissionsFromRoleUseCase
    {
        Task<bool> Execute(int roleId, RoleSubModulePermissionUpdateRequest request);
    }
}
