using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Users;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.DTOs;

namespace MyApp.Application.UseCases.Users
{
    public class UserGetAllUseCase : IUserGetAllUseCase
    {
        private readonly IGenericRepository<UsersEntity> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserGetAllUseCase> _logger;

        public UserGetAllUseCase(
            IGenericRepository<UsersEntity> userRepository,
            IMapper mapper,
            ILogger<UserGetAllUseCase> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResult<UserResponse>> Execute(int page, int size)
        {
            _logger.LogInformation("Iniciando la obtención de todos los usuarios.");

            var (items, totalCount) = await _userRepository.Pagination(page, size);

            var mappedItems = _mapper.Map<IEnumerable<UserResponse>>(items);

            _logger.LogInformation("Se obtuvieron {Count} usuarios.", totalCount);

            return new PaginationResult<UserResponse>
            {
                RowsCount = totalCount,
                PageCount = (int)Math.Ceiling((double)totalCount / size),
                PageSize = size,
                CurrentPage = page,
                Results = mappedItems
            };
        }
    }
}
