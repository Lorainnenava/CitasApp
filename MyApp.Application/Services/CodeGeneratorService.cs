using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;

namespace MyApp.Application.Services
{
    public class CodeGeneratorService : ICodeGeneratorService
    {
        private readonly IGenericRepository<UsersEntity> _repository;
        private readonly Random _random = new();

        public CodeGeneratorService(IGenericRepository<UsersEntity> repository)
        {
            _repository = repository;
        }

        public async Task<string> GenerateUniqueCode()
        {
            string code;
            UsersEntity? exists;

            do
            {
                code = _random.Next(10000, 100000).ToString();
                exists = await _repository.GetByCondition(x => x.CodeValidation == code);
            }
            while (exists != null);

            return code;
        }
    }
}
