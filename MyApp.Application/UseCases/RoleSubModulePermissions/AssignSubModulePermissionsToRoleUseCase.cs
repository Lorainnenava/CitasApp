using AutoMapper;
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
    public class AssignSubModulePermissionsToRoleUseCase : IAssignSubModulePermissionsToRoleUseCase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<RolesEntity> _rolesRepository;
        private readonly ILogger<AssignSubModulePermissionsToRoleUseCase> _logger;
        private readonly IGenericRepository<SubmodulePermissionsEntity> _subModulePermissionRepository;
        private readonly IGenericRepository<RoleSubModulePermissionsEntity> _roleSubModulePermissionsRepository;

        public AssignSubModulePermissionsToRoleUseCase(
            IMapper mapper,
            IGenericRepository<RolesEntity> rolesRepository,
            IGenericRepository<SubmodulePermissionsEntity> subModulePermissionRepository,
            IGenericRepository<RoleSubModulePermissionsEntity> roleSubModulePermissionsRepository,
            ILogger<AssignSubModulePermissionsToRoleUseCase> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _rolesRepository = rolesRepository;
            _subModulePermissionRepository = subModulePermissionRepository;
            _roleSubModulePermissionsRepository = roleSubModulePermissionsRepository;
        }

        public async Task<List<RoleSubModuleResponse>> Execute(RoleSubModulePermissionAssignToRoleRequest request)
        {
            _logger.LogInformation("Iniciando Asignacción de los permisos de los submódulos del rol con ID: {RoleId}", request.RoleId);

            ValidatorHelper.ValidateAndThrow(request, new RoleSubModulePermissionAssignToRoleValidator());

            var roleExisted = await _rolesRepository.GetByCondition(x => x.RoleId == request.RoleId, x => x.RoleSubModulePermissions);

            if (roleExisted is null)
            {
                _logger.LogWarning("No se encontró el rol con el ID: {RoleId}", request.RoleId);
                throw new NotFoundException("El rol que estas buscando no existe.");
            }

            if (roleExisted.RoleSubModulePermissions.Count > 0)
            {
                _logger.LogWarning("Ya existen permisos asignados al rol con ID: {RoleId}", request.RoleId);
                throw new NotFoundException("Este rol ya tiene permisos asignados para este submódulo. Por favor, utiliza la opción de actualización.");
            }

            var subModulePermissions = await _subModulePermissionRepository.GetAll();
            var existingSubModulePermissionIds = subModulePermissions.Select(p => p.PermissionId).ToHashSet();

            var createdPermissions = new List<RoleSubModuleResponse>();
            var dbContext = _rolesRepository.GetDbContext();

            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                foreach (var subModulepermission in request.SubModulePermissions)
                {
                    if (!existingSubModulePermissionIds.Contains(subModulepermission.SubModulePermissionId))
                    {
                        _logger.LogWarning("No se encontró el permiso de submódulo con ID: {SubModulePermissionId}", subModulepermission.SubModulePermissionId);
                        throw new NotFoundException("El permiso de submódulo que estás intentando asignar a este rol no existe.");
                    }

                    var submoduloPermission = new RoleSubModulePermissionsEntity
                    {
                        RoleId = request.RoleId,
                        SubModulePermissionId = subModulepermission.SubModulePermissionId,
                    };

                    var createEntity = await _roleSubModulePermissionsRepository.Create(submoduloPermission);
                    var mapped = _mapper.Map<RoleSubModuleResponse>(createEntity);
                    createdPermissions.Add(mapped);
                }

                await transaction.CommitAsync();
                _logger.LogInformation("Permisos asigandos correctamente al rol con ID: {RoleId}", request.RoleId);
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
