using AutoMapper;
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
    public class AssignPermissionsToSubModuleUseCase : IAssignPermissionsToSubModuleUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AssignPermissionsToSubModuleUseCase> _logger;
        private readonly IGenericRepository<SubModulesEntity> _subModulesRepository;
        private readonly IGenericRepository<PermissionsEntity> _permissionRepository;
        private readonly IGenericRepository<SubmodulePermissionsEntity> _subModulePermissionsRepository;

        public AssignPermissionsToSubModuleUseCase(
            IMapper mapper,
            ILogger<AssignPermissionsToSubModuleUseCase> logger,
            IGenericRepository<SubModulesEntity> subModulesRepository,
            IGenericRepository<PermissionsEntity> permissionRepository,
            IGenericRepository<SubmodulePermissionsEntity> subModulePermissionsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _subModulesRepository = subModulesRepository;
            _permissionRepository = permissionRepository;
            _subModulePermissionsRepository = subModulePermissionsRepository;
        }

        public async Task<List<SubModulePermissionAssignToSubModuleResponse>> Execute(SubModulePermissionAssignToSubModuleRequest request)
        {
            _logger.LogInformation("Iniciando Asignacción de permisos del submódulo con ID: {SubModuleId}", request.SubModuleId);

            ValidatorHelper.ValidateAndThrow(request, new SubModulePermissionAssignToSubModuleValidator());

            var subModule = await _subModulesRepository.GetByCondition(x => x.SubModuleId == request.SubModuleId && x.IsActive == true, x => x.SubModulePermissions);

            if (subModule is null)
            {
                _logger.LogWarning("No se encontró el submódulo con el ID: {SubModuleId}", request.SubModuleId);
                throw new NotFoundException("El submódulo que estas buscando no existe o esta inactivo.");
            }

            if (subModule.SubModulePermissions.Count > 0)
            {
                _logger.LogWarning("Se encontraron permisos existentes para el submódulo con ID: {SubModuleId}", request.SubModuleId);
                throw new NotFoundException("Ya existen permisos registrados para este submódulo. Por favor, utiliza la opción de actualización.");
            }

            var permissions = await _permissionRepository.GetAll();
            var existingPermissionIds = permissions.Select(p => p.PermissionId).ToHashSet();

            var createdPermissions = new List<SubModulePermissionAssignToSubModuleResponse>();
            var dbContext = _subModulePermissionsRepository.GetDbContext();

            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                foreach (var permission in request.Permissions)
                {
                    if (!existingPermissionIds.Contains(permission.PermissionId))
                    {
                        _logger.LogWarning("No se encontró el permiso con ID: {PermissionId}", permission.PermissionId);
                        throw new NotFoundException("El permiso que estás intentando asignar a este submódulo no existe.");
                    }

                    var submoduloPermission = new SubmodulePermissionsEntity
                    {
                        SubModuleId = request.SubModuleId,
                        PermissionId = permission.PermissionId
                    };

                    var createEntity = await _subModulePermissionsRepository.Create(submoduloPermission);
                    var mapped = _mapper.Map<SubModulePermissionAssignToSubModuleResponse>(createEntity);
                    createdPermissions.Add(mapped);
                }

                await transaction.CommitAsync();

                _logger.LogInformation("Permisos asigandos correctamente al submódulo con ID: {SubModuleId}", request.SubModuleId);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Ocurrió un error durante la asignación de permisos. Se realizó rollback.");
                throw;
            }

            return createdPermissions;
        }
    }
}
