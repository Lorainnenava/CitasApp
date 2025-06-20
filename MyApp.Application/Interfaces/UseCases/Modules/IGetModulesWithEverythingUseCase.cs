using MyApp.Application.DTOs.Modules;

namespace MyApp.Application.Interfaces.UseCases.Modules
{
    public interface IGetModulesWithEverythingUseCase
    {
        Task<IEnumerable<ModuleResponse>> Execute();
    }
}
