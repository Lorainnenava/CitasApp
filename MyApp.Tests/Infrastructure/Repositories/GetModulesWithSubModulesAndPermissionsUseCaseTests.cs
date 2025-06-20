using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Context;
using MyApp.Infrastructure.Repositories;

namespace MyApp.Tests.Infrastructure.Repositories
{
    public class ModuleRepositoryTests
    {
        private static ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + System.Guid.NewGuid())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task GetModulesWithSubModulesAndPermissions_ShouldReturnModulesWithNestedData()
        {
            using var dbContext = GetInMemoryDbContext();

            var permission = new PermissionsEntity { PermissionId = 1, Name = "Ver" };
            var subModulePermission = new SubmodulePermissionsEntity
            {
                SubModuleId = 1,
                PermissionId = 1,
                SubModulePermissionId = 1,
            };
            var subModule = new SubModulesEntity
            {
                SubModuleId = 1,
                Name = "Submódulo A",
                SubModulePermissions = [subModulePermission],
            };
            var module = new ModulesEntity
            {
                ModuleId = 1,
                Name = "Módulo A",
                SubModules = [subModule]
            };

            dbContext.Permissions.Add(permission);
            dbContext.Modules.Add(module);
            await dbContext.SaveChangesAsync();

            var repository = new ModuleRepository(dbContext);

            var result = await repository.GetModulesWithSubModulesAndPermissions();

            Assert.NotNull(result);
            var moduleResult = result.FirstOrDefault();
            Assert.NotNull(moduleResult);
            Assert.Equal("Módulo A", moduleResult.Name);
            Assert.Single(moduleResult.SubModules);
            Assert.Single(moduleResult.SubModules.First().SubModulePermissions);
            Assert.Equal("Ver", moduleResult.SubModules.First().SubModulePermissions.First().Permission.Name);
        }

        [Fact]
        public async Task GetModulesWithSubModulesAndPermissions_ShouldReturnEmpty_WhenNoData()
        {
            using var dbContext = GetInMemoryDbContext();
            var repository = new ModuleRepository(dbContext);

            var result = await repository.GetModulesWithSubModulesAndPermissions();

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
