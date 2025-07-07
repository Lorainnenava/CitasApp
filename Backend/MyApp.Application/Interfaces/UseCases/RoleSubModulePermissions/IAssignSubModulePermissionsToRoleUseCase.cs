using MyApp.Application.DTOs.RoleSubModulePermissions;

namespace MyApp.Application.Interfaces.UseCases.RoleSubModulePermissions
{
    public interface IAssignSubModulePermissionsToRoleUseCase
    {
        Task<List<RoleSubModuleResponse>> Execute(RoleSubModulePermissionAssignToRoleRequest request);
    }
}
