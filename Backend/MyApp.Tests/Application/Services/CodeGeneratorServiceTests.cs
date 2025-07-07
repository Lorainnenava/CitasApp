using Moq;
using MyApp.Application.Services;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;

namespace MyApp.Tests.Application.Services
{
    public class CodeGeneratorServiceTests
    {
        private readonly Mock<IGenericRepository<UsersEntity>> _repositoryMock;
        private readonly CodeGeneratorService _service;

        public CodeGeneratorServiceTests()
        {
            _repositoryMock = new Mock<IGenericRepository<UsersEntity>>();
            _service = new CodeGeneratorService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GenerateUniqueCode_ShouldReturnUniqueCode()
        {
            _repositoryMock
                .Setup(repo => repo.GetByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync((UsersEntity)null!);

            string code = await _service.GenerateUniqueCode();

            Assert.NotNull(code);
            Assert.Matches(@"^\d{5}$", code);
        }

        [Fact]
        public async Task GenerateUniqueCode_ShouldRetryIfCodeAlreadyExists()
        {
            var existingUser = new UsersEntity { CodeValidation = "12345" };

            _repositoryMock
                .SetupSequence(repo => repo.GetByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<UsersEntity, bool>>>()))
                .ReturnsAsync(existingUser)
                .ReturnsAsync((UsersEntity)null!);

            string code = await _service.GenerateUniqueCode();

            Assert.NotNull(code);
            Assert.Matches(@"^\d{5}$", code);
            _repositoryMock.Verify(repo => repo.GetByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<UsersEntity, bool>>>()), Times.Exactly(2));
        }
    }
}
