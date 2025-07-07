using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.SubModulePermissions;
using MyApp.Application.Interfaces.UseCases.SubModulePermissions;
using MyApp.Application.Validators.SubModulePermissions;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Shared.Services;

namespace MyApp.Application.UseCases.SubModulePermissions
{
    public class UpdatePermissionFromSubModuloUseCase : IUpdatePermissionsFromSubModuleUseCase
    {
        public readonly IGenericRepository<SubModulesEntity> _subModuleRepository;
        public readonly IGenericRepository<SubmodulePermissionsEntity> _subModulePermissionRepository;
        public readonly IGenericRepository<PermissionsEntity> _permissionRepository;
        public readonly ILogger<UpdatePermissionFromSubModuloUseCase> _logger;

        public UpdatePermissionFromSubModuloUseCase(
            IGenericRepository<SubModulesEntity> subModuleRepository,
            IGenericRepository<SubmodulePermissionsEntity> subModulePermissionRepository,
            IGenericRepository<PermissionsEntity> permissionRepository,
            ILogger<UpdatePermissionFromSubModuloUseCase> logger)
        {
            _logger = logger;
            _subModuleRepository = subModuleRepository;
            _permissionRepository = permissionRepository;
            _subModulePermissionRepository = subModulePermissionRepository;
        }

        public async Task<bool> Execute(int SubModuleId, SubModulePermissionUpdateRequest request)
        {
            _logger.LogInformation("Iniciando proceso para actualizar permisos del submódulo con el ID: {SubModuleId}", SubModuleId);

            var validator = new SubModulePermissionUpdateValidator();
            ValidatorHelper.ValidateAndThrow(request, validator);

            var subModule = await _subModuleRepository.GetByCondition(x => x.SubModuleId == SubModuleId && x.IsActive, x => x.SubModulePermissions);

            if (subModule is null)
            {
                _logger.LogWarning("No se encontró el submódulo con el ID: {SubModuleId}", SubModuleId);
                throw new NotFoundException("El submódulo que estas buscando no existe o esta inactivo.");
            }

            var subModulePermissions = await _subModulePermissionRepository.GetAll(x => x.SubModuleId == SubModuleId);

            if (!subModulePermissions.Any())
            {
                _logger.LogWarning("No se encontraron permisos asociados al submódulo con ID: {SubModuleId}", SubModuleId);
                throw new NotFoundException("No existen permisos registrados para este submódulo.");
            }

            _logger.LogInformation("Actualizando permisos para el submódulo con el ID: {SubModuleId}", SubModuleId);

            var dbContext = _subModulePermissionRepository.GetDbContext();

            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                foreach (var subModulePermission in subModulePermissions)
                {
                    var shouldBeActive = request.ActivePermissionIds
                        .Any(x => x.SubModulePermissionId == subModulePermission.SubModulePermissionId);

                    subModulePermission.IsActive = shouldBeActive;
                    await _subModulePermissionRepository.Update(subModulePermission);
                }

                var newPermissionIds = request.NewPermissionIdsToAssign.Select(x => x.PermissionId).ToList();
                var existingPermissions = await _permissionRepository.GetAll(x => newPermissionIds.Contains(x.PermissionId));
                var existingPermissionIds = existingPermissions.Select(p => p.PermissionId).ToHashSet();

                foreach (var newSubModulePermission in request.NewPermissionIdsToAssign)
                {
                    if (!existingPermissionIds.Contains(newSubModulePermission.PermissionId))
                    {
                        _logger.LogWarning("No se encontró el permiso con ID: {PermissionId}", newSubModulePermission.PermissionId);
                        throw new NotFoundException("El permiso que estás intentando asignar a este submódulo no existe.");
                    }

                    var existingPermission = subModulePermissions.FirstOrDefault(x => x.PermissionId == newSubModulePermission.PermissionId);

                    if (existingPermission is null)
                    {
                        var newSubModulePermissionEntity = new SubmodulePermissionsEntity
                        {
                            SubModuleId = SubModuleId,
                            PermissionId = newSubModulePermission.PermissionId,
                            IsActive = true
                        };

                        await _subModulePermissionRepository.Create(newSubModulePermissionEntity);
                    }
                    else if (!existingPermission.IsActive)
                    {
                        existingPermission.IsActive = true;
                        await _subModulePermissionRepository.Update(existingPermission);
                    }
                }

                await transaction.CommitAsync();
                _logger.LogInformation("Proceso de actualización de permisos finalizado para el submódulo con el ID: {SubModuleId}", SubModuleId);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Ocurrió un error durante la actualización de permisos. Se realizó rollback.");
                throw;
            }

            return true;
        }
    }
}
