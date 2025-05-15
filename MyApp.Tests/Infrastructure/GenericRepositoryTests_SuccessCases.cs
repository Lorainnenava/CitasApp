using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Context;
using MyApp.Infrastructure.Repositories;
using MyApp.Tests.Mocks;

namespace MyApp.Tests.Infrastructure
{
    public class GenericRepositoryTests_SuccessCases
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly GenericRepository<UsersEntity> _genericRepository;
        private readonly List<UsersEntity> _fakeData;

        public GenericRepositoryTests_SuccessCases()
        {
            _dbContext = GetDbContext();
            _fakeData = MockUser.MockListUsersEntity();
            _dbContext.Users.AddRange(_fakeData);
            _dbContext.SaveChanges();
            _genericRepository = new GenericRepository<UsersEntity>(_dbContext);
        }

        private static ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Create_ShouldAddNewEntity_WhenValidDataProvided()
        {
            var entityToCreate = MockUser.MockOneUserEntityToCreate();

            var result = await _genericRepository.Create(entityToCreate);

            Assert.NotNull(result);
            Assert.Equal(3, _dbContext.Users.Count());
            Assert.Equal("NuevoUsuario", result.Name);
            Assert.True(await _dbContext.Users.AnyAsync(p => p.Name == "NuevoUsuario"));
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity_WhenEntityExists()
        {
            bool result = await _genericRepository.Delete(x => x.UserName == "Prueba123");

            Assert.True(result);
            Assert.Equal(1, _dbContext.Users.Count());
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEntities_WhenCalled()
        {
            IEnumerable<UsersEntity> result = await _genericRepository.GetAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Collection(result,
                item => Assert.Equal("Prueba123", item.UserName),
                item => Assert.Equal("Jane", item.FirstName)
            );

        }

        [Fact]
        public async Task Update_ShouldModifyEntity_WhenEntityExists()
        {
            var entityToUpdate = MockUser.MockOneUserEntityUpdated();

            UsersEntity? result = await _genericRepository.Update(x => x.UserName == "DevJane", entityToUpdate);

            Assert.NotNull(result);
            Assert.Equal("jane.doe567@example.com", result.Email);
            Assert.True(await _dbContext.Users.AnyAsync(p => p.Email == "jane.doe567@example.com"));
        }
    }
}