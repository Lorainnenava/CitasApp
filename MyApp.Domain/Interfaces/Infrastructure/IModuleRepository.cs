using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces.Infrastructure
{
    public interface IModuleRepository : IGenericRepository<ModulesEntity>
    {
        Task<IEnumerable<ModulesEntity>> GetModulesWithSubModulesAndPermissions();
    }
}
