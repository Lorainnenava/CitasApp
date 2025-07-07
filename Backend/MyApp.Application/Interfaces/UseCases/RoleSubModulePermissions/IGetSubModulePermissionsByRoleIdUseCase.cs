using MyApp.Application.DTOs.RoleSubModulePermissions;

namespace MyApp.Application.Interfaces.UseCases.RoleSubModulePermissions
{
    public interface IGetSubModulePermissionsByRoleIdUseCase
    {
        Task<List<RoleSubModuleResponse>> Execute(int RoleId);
    }
}
