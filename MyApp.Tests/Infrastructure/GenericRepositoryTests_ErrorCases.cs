using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Context;
using MyApp.Infrastructure.Repositories;
using MyApp.Shared.Exceptions;
using MyApp.Tests.Mocks;

namespace MyApp.Tests.Infrastructure
{
    public class GenericRepositoryTests_ErrorCases
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly GenericRepository<UsersEntity> _genericRepository;

        public GenericRepositoryTests_ErrorCases()
        {
            _dbContext = GetDbContext();
            _genericRepository = new GenericRepository<UsersEntity>(_dbContext);
        }

        private static ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        internal void SeedDatabaseWithFakeData()
        {
            var fakeData = MockUser.MockListUsersEntity();
            _dbContext.Users.AddRange(fakeData);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenEntityDoesNotExist()
        {
            SeedDatabaseWithFakeData();

            var resultado = await _genericRepository.Delete(x => x.FirstName == "Usuario");

            Assert.False(resultado);
            Assert.Equal(2, _dbContext.Users.Count());
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyList_WhenNoEntitiesExist()
        {
            IEnumerable<UsersEntity> result = await _genericRepository.GetAll();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Update_ShouldThrowNotFoundException_WhenEntityDoesNotExist()
        {
            SeedDatabaseWithFakeData();

            var updatedData = MockUser.MockOneUserEntityUpdated();

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                _genericRepository.Update(updatedData));

            Assert.Equal("Item no encontrado", exception.Message);
        }
    }
}