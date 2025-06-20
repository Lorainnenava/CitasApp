using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Infrastructure.Context;

namespace MyApp.Infrastructure.Repositories
{
    public class ModuleRepository : GenericRepository<ModulesEntity>, IModuleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ModuleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ModulesEntity>> GetModulesWithSubModulesAndPermissions()
        {
            return await _dbContext.Modules
                .Include(m => m.SubModules)
                    .ThenInclude(sm => sm.SubModulePermissions)
                    .ThenInclude(smp => smp.Permission)
                .ToListAsync();
        }
    }
}
