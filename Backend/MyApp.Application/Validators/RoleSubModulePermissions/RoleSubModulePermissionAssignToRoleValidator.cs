using FluentValidation;
using MyApp.Application.DTOs.RoleSubModulePermissions;

namespace MyApp.Application.Validators.RoleSubModulePermissions
{
    public class RoleSubModulePermissionAssignToRoleValidator : AbstractValidator<RoleSubModulePermissionAssignToRoleRequest>
    {
        public RoleSubModulePermissionAssignToRoleValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty()
                .WithMessage("El campo RoleId no puede estar vacío.");
            RuleFor(x => x.SubModulePermissions)
                .NotEmpty()
                .WithMessage("La lista de SubModulePermissions no puede estar vacía.")
                .Must(permissions => permissions.All(p => p.SubModulePermissionId > 0))
                .WithMessage("Todos los SubModulePermissions deben tener un ID válido.");
        }
    }
}
