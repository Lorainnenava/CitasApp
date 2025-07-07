using FluentValidation;
using MyApp.Application.DTOs.RoleSubModulePermissions;

namespace MyApp.Application.Validators.RoleSubModulePermissions
{
    public class RoleSubModulePermissionUpdateValidator : AbstractValidator<RoleSubModulePermissionUpdateRequest>
    {
        public RoleSubModulePermissionUpdateValidator()
        {
            RuleFor(x => x.ActiveRoleSubModulePermissionIds)
                .NotNull()
                .WithMessage("La lista de permisos activos no puede ser nula.")
                .Must(x => x.All(p => p.RoleSubModulePermisssionId > 0))
                .WithMessage("Todos los RoleSubModulePermisssionId deben ser mayores a cero.")
                .Must(x => x.Select(p => p.RoleSubModulePermisssionId).Distinct().Count() == x.Count)
                .WithMessage("No se permiten RoleSubModulePermisssionId duplicados.");

            RuleFor(x => x.NewRoleSubModulePermissionIdsToAssign)
                .NotNull()
                .WithMessage("La lista de nuevos permisos no puede ser nula.")
                .Must(x => x.All(p => p.SubModulePermissionId > 0))
                .WithMessage("Todos los SubModulePermissionId deben ser mayores a cero.")
                .Must(x => x.Select(p => p.SubModulePermissionId).Distinct().Count() == x.Count)
                .WithMessage("No se permiten SubModulePermissionId duplicados.");
        }
    }
}
