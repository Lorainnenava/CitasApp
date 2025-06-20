using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.RoleSubModulePermissions;
using MyApp.Application.Interfaces.UseCases.RoleSubModulePermissions;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.RoleSubModulePermissions
{
    public class GetSubModulePermissionsByRoleIdUseCase : IGetSubModulePermissionsByRoleIdUseCase
    {
        public readonly IGenericRepository<RolesEntity> _roleRepository;
        public readonly ILogger<GetSubModulePermissionsByRoleIdUseCase> _logger;

        public GetSubModulePermissionsByRoleIdUseCase(
            IGenericRepository<RolesEntity> roleRepository,
            ILogger<GetSubModulePermissionsByRoleIdUseCase> logger)
        {
            _logger = logger;
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleSubModuleResponse>> Execute(int RoleId)
        {
            _logger.LogInformation("Obteniendo permisos de submódulos para el rol con ID: {RoleId}", RoleId);

            var roleExisted = await _roleRepository.GetByCondition(x => x.RoleId == RoleId, x => x.RoleSubModulePermissions);

            if (roleExisted is null)
            {
                _logger.LogWarning("No se encontró el rol con ID: {RoleId}", RoleId);
                throw new NotFoundException("El rol que estas buscando no existe.");
            }

            if (roleExisted.RoleSubModulePermissions.Count == 0)
            {
                _logger.LogWarning("No se encontraron permisos de submódulo para el rol con ID: {RoleId}", RoleId);
                throw new NotFoundException("No se encontraron permisos de submódulo para el rol especificado.");
            }

            var response = roleExisted.RoleSubModulePermissions
                .Where(x => x.IsActive)
                .Select(x => new RoleSubModuleResponse
                {
                    RoleId = RoleId,
                    SubModulePermissionId = x.SubModulePermissionId,
                    RoleSubModulePermissionId = x.RoleSubModulePermissionId,
                })
                .ToList();

            _logger.LogInformation("Permisos de submódulo encontrados exitosamente para el rol con ID: {RoleId}", RoleId);

            return response;
        }
    }
}
