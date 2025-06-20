using MyApp.Application.DTOs.SubModulePermissions;

namespace MyApp.Application.Interfaces.UseCases.SubModulePermissions
{
    public interface IAssignPermissionsToSubModuleUseCase
    {
        Task<List<SubModulePermissionAssignToSubModuleResponse>> Execute(SubModulePermissionAssignToSubModuleRequest request);
    }
}
