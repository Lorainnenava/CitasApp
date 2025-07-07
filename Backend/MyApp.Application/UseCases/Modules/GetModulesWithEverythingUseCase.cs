using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Modules;
using MyApp.Application.DTOs.SubModulePermissions;
using MyApp.Application.DTOs.SubModules;
using MyApp.Application.Interfaces.UseCases.Modules;
using MyApp.Domain.Interfaces.Infrastructure;

namespace MyApp.Application.UseCases.Modules
{
    public class GetModulesWithEverythingUseCase : IGetModulesWithEverythingUseCase
    {
        public readonly IModuleRepository _moduleRepository;
        public readonly ILogger<GetModulesWithEverythingUseCase> _logger;

        public GetModulesWithEverythingUseCase(
            IModuleRepository moduleRepository,
            ILogger<GetModulesWithEverythingUseCase> logger)
        {
            _logger = logger;
            _moduleRepository = moduleRepository;
        }

        public async Task<IEnumerable<ModuleResponse>> Execute()
        {
            _logger.LogInformation("Iniciando la obtención de los modulos con sus submodulos y permisos correspondientes.");

            var modules = await _moduleRepository.GetModulesWithSubModulesAndPermissions();

            List<ModuleResponse> response = [];

            _logger.LogInformation("Se obtuvieron {modules} modulos con sus submodulos y permisos.", modules.Count());

            foreach (var module in modules.Where(x => x.IsActive))
            {
                var newModule = new ModuleResponse
                {
                    ModuleId = module.ModuleId,
                    Name = module.Name,
                    SubModules = [.. module.SubModules.Where(x=> x.IsActive).Select(subModule => new SubModuleResponse
                    {
                        SubModuleId = subModule.SubModuleId,
                        Name = subModule.Name,
                        SubModulePermissions = [.. subModule.SubModulePermissions
                        .Where(x=> x.IsActive)
                        .Select(sp => new SubModulePermissionResponse
                        {
                            SubModulePermissionId = sp.SubModulePermissionId,
                            Name = sp.Permission.Name
                        })]
                    })]
                };

                response.Add(newModule);
            }

            _logger.LogInformation("Finalizada la obtención de los modulos con sus submodulos y permisos correspondientes.");

            return [.. response];
        }
    }
}
