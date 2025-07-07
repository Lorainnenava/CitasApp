using FluentValidation;
using MyApp.Application.DTOs.SubModulePermissions;

namespace MyApp.Application.Validators.SubModulePermissions
{
    public class SubModulePermissionAssignToSubModuleValidator : AbstractValidator<SubModulePermissionAssignToSubModuleRequest>
    {
        public SubModulePermissionAssignToSubModuleValidator()
        {
            RuleFor(x => x.SubModuleId)
                .NotEmpty()
                .WithMessage("El campo SubModuleId no puede estar vacío.");
            RuleFor(x => x.Permissions)
                .NotEmpty()
                .WithMessage("La lista de permisos no puede estar vacía.")
                .Must(permissions => permissions.All(p => p.PermissionId > 0))
                .WithMessage("Todos los permisos deben tener un ID válido.");
        }
    }
}
