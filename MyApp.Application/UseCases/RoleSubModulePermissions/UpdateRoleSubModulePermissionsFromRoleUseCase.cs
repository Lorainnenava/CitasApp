using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.RoleSubModulePermissions;
using MyApp.Application.Interfaces.UseCases.RoleSubModulePermissions;
using MyApp.Application.Validators.RoleSubModulePermissions;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Shared.Services;

namespace MyApp.Application.UseCases.RoleSubModulePermissions
{
    public class UpdateRoleSubModulePermissionsFromRoleUseCase : IUpdateRoleSubModulePermissionsFromRoleUseCase
    {
        public readonly IGenericRepository<RolesEntity> _roleRepo;
        public readonly IGenericRepository<SubmodulePermissionsEntity> _subModulePermissionRepo;
        public readonly IGenericRepository<RoleSubModulePermissionsEntity> _roleSubModulePermissionRepo;
        public readonly IGenericRepository<PermissionsEntity> _permissionRepo;
        public readonly ILogger<UpdateRoleSubModulePermissionsFromRoleUseCase> _logger;

        public UpdateRoleSubModulePermissionsFromRoleUseCase(
            IGenericRepository<RolesEntity> roleRepo,
            IGenericRepository<SubmodulePermissionsEntity> subModulePermissionRepo,
            IGenericRepository<RoleSubModulePermissionsEntity> roleSubModulePermissionRepo,
            IGenericRepository<PermissionsEntity> permissionRepo,
            ILogger<UpdateRoleSubModulePermissionsFromRoleUseCase> logger)
        {
            _logger = logger;
            _roleRepo = roleRepo;
            _permissionRepo = permissionRepo;
            _subModulePermissionRepo = subModulePermissionRepo;
            _roleSubModulePermissionRepo = roleSubModulePermissionRepo;
        }

        public async Task<bool> Execute(int roleId, RoleSubModulePermissionUpdateRequest request)
        {
            _logger.LogInformation("Iniciando proceso para actualizar permisos de submódulo para el rol con el ID: {roleId}", roleId);

            ValidatorHelper.ValidateAndThrow(request, new RoleSubModulePermissionUpdateValidator());

            var roleWithPermissions = await _roleRepo.GetByCondition(x => x.RoleId == roleId, x => x.RoleSubModulePermissions);

            if (roleWithPermissions is null)
            {
                _logger.LogWarning("No se encontró el rol con el ID: {roleId}", roleId);
                throw new NotFoundException("El rol que estás buscando no existe.");
            }

            var existingRolePermissions = await _roleSubModulePermissionRepo.GetAll(x => x.RoleId == roleId);

            if (!existingRolePermissions.Any())
            {
                _logger.LogWarning("No se encontraron permisos asociados al submódulo con ID: {roleId}", roleId);
                throw new NotFoundException("No existen permisos registrados para este submódulo.");
            }

            _logger.LogInformation("Actualizando permisos para el submódulo con el ID: {roleId}", roleId);

            var dbContext = _subModulePermissionRepo.GetDbContext();
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                foreach (var permission in existingRolePermissions)
                {
                    var isStillActive = request.ActiveRoleSubModulePermissionIds
                        .Any(x => x.RoleSubModulePermisssionId == permission.RoleSubModulePermissionId);

                    permission.IsActive = isStillActive;
                    await _roleSubModulePermissionRepo.Update(permission);
                }

                var newPermissionIds = request.NewRoleSubModulePermissionIdsToAssign.Select(x => x.SubModulePermissionId).ToList();
                var validPermissions = await _subModulePermissionRepo.GetAll(x => newPermissionIds.Contains(x.SubModulePermissionId));
                var validPermissionIds = validPermissions.Select(p => p.SubModulePermissionId).ToHashSet();

                foreach (var newPermission in request.NewRoleSubModulePermissionIdsToAssign)
                {
                    if (!validPermissionIds.Contains(newPermission.SubModulePermissionId))
                    {
                        _logger.LogWarning("No se encontró el SubModulePermission con ID: {SubModulePermissionId}", newPermission.SubModulePermissionId);
                        throw new NotFoundException("El permiso de submódulo que estás intentando asignar a este rol no existe.");
                    }

                    var existing = existingRolePermissions
                        .FirstOrDefault(x => x.SubModulePermissionId == newPermission.SubModulePermissionId);

                    if (existing is null)
                    {
                        var newEntity = new RoleSubModulePermissionsEntity
                        {
                            RoleId = roleId,
                            SubModulePermissionId = newPermission.SubModulePermissionId,
                            IsActive = true
                        };

                        await _roleSubModulePermissionRepo.Create(newEntity);
                    }
                    else if (!existing.IsActive)
                    {
                        existing.IsActive = true;
                        await _roleSubModulePermissionRepo.Update(existing);
                    }
                }

                await transaction.CommitAsync();
                _logger.LogInformation("Proceso de actualización de permisos de submódulo finalizado para el rol con el ID: {roleId}", roleId);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Ocurrió un error durante la actualización de permisos de submódulo. Se realizó rollback.");
                throw;
            }

            return true;
        }
    }
}