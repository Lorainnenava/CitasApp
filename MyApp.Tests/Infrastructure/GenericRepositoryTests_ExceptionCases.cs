using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Context;
using MyApp.Infrastructure.Repositories;
using MyApp.Tests.Mocks;

namespace MyApp.Tests.Infrastructure
{
    public class GenericRepositoryTests_ExceptionCases
    {
        private readonly ApplicationDbContext _faultyContext;
        private readonly GenericRepository<UsersEntity> _faultyRepository;

        public GenericRepositoryTests_ExceptionCases()
        {
            _faultyContext = GetFaultyDbContext();
            _faultyRepository = new GenericRepository<UsersEntity>(_faultyContext);
        }

        private static ApplicationDbContext GetFaultyDbContext()
        {
            // Se crea un DbContext sin opciones configuradas (sin proveedor de base de datos)
            // para simular un contexto mal configurado que lance excepciones al usarse.
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Create_ShouldThrowInvalidOperationException_WhenDbContextIsMisconfigured()
        {
            var userToCreate = MockUser.MockOneUserEntity();

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _faultyRepository.Create(userToCreate));
        }

        [Fact]
        public async Task Delete_ShouldThrowInvalidOperationException_WhenDbContextIsMisconfigured()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _faultyRepository.Delete(u => u.FirstName == "PruebaError"));
        }

        [Fact]
        public async Task GetAll_ShouldThrowInvalidOperationException_WhenDbContextIsMisconfigured()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _faultyRepository.GetAll());
        }

        [Fact]
        public async Task Update_ShouldThrowInvalidOperationException_WhenDbContextIsMisconfigured()
        {
            var userToUpdate = MockUser.MockOneUserEntityUpdated();

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _faultyRepository.Update(userToUpdate));
        }
    }
}