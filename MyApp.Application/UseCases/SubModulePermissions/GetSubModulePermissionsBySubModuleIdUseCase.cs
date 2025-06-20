using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.SubModulePermissions;
using MyApp.Application.Interfaces.UseCases.SubModulePermissions;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.SubModulePermissions
{
    public class GetSubModulePermissionsBySubModuleIdUseCase : IGetSubModulePermissionsBySubModuleIdUseCase
    {
        public readonly IGenericRepository<SubModulesEntity> _subModulesRepository;
        public readonly ILogger<GetSubModulePermissionsBySubModuleIdUseCase> _logger;

        public GetSubModulePermissionsBySubModuleIdUseCase(
            IGenericRepository<SubModulesEntity> subModulesRepository,
            ILogger<GetSubModulePermissionsBySubModuleIdUseCase> logger)
        {
            _logger = logger;
            _subModulesRepository = subModulesRepository;
        }

        public async Task<List<SubModulePermissionGetResponse>> Execute(int SubModuleId)
        {
            _logger.LogInformation("Obteniendo permisos para el submódulo con ID: {SubModuleId}", SubModuleId);

            var subModule = await _subModulesRepository.GetByCondition(x => x.SubModuleId == SubModuleId && x.IsActive, x => x.SubModulePermissions);

            if (subModule is null)
            {
                _logger.LogWarning("No se encontró el submódulo con ID: {SubModuleId}", SubModuleId);
                throw new NotFoundException("El submódulo que estas buscando no existe o esta inactivo.");
            }

            if (subModule.SubModulePermissions.Count == 0)
            {
                _logger.LogWarning("No se encontraron permisos para el submódulo con ID: {SubModuleId}", SubModuleId);
                throw new NotFoundException("No se encontraron permisos para el submódulo especificado.");
            }

            var response = subModule.SubModulePermissions
                .Where(x => x.IsActive)
                .Select(x => new SubModulePermissionGetResponse
                {
                    SubModulePermissionId = x.SubModulePermissionId,
                    PermissionId = x.PermissionId
                })
                .ToList();

            _logger.LogInformation("Permisos encontrados exitosamente para el submódulo con ID: {SubModuleId}", SubModuleId);

            return response;
        }
    }
}
