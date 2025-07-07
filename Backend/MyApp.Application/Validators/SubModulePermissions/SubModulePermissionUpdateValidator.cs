using FluentValidation;
using MyApp.Application.DTOs.SubModulePermissions;

namespace MyApp.Application.Validators.SubModulePermissions
{
    public class SubModulePermissionUpdateValidator : AbstractValidator<SubModulePermissionUpdateRequest>
    {
        public SubModulePermissionUpdateValidator()
        {
            RuleFor(x => x.ActivePermissionIds)
                .NotNull()
                .WithMessage("La lista de permisos activos no puede ser nula.")
                .Must(x => x.All(p => p.SubModulePermissionId > 0))
                .WithMessage("Todos los SubModulePermissionId deben ser mayores a cero.")
                .Must(x => x.Select(p => p.SubModulePermissionId).Distinct().Count() == x.Count)
                .WithMessage("No se permiten SubModulePermissionId duplicados.");

            RuleFor(x => x.NewPermissionIdsToAssign)
                .NotNull()
                .WithMessage("La lista de nuevos permisos no puede ser nula.")
                .Must(x => x.All(p => p.PermissionId > 0))
                .WithMessage("Todos los PermissionId deben ser mayores a cero.")
                .Must(x => x.Select(p => p.PermissionId).Distinct().Count() == x.Count)
                .WithMessage("No se permiten PermissionId duplicados.");
        }
    }
}
