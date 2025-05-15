using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Users;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Users
{
    public class UserGetByIdUseCase : IUserGetByIdUseCase
    {
        private readonly IGenericRepository<UsersEntity> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserGetByIdUseCase> _logger;

        public UserGetByIdUseCase(
            IGenericRepository<UsersEntity> userRepository,
            IMapper mapper,
            ILogger<UserGetByIdUseCase> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserResponse> Execute(int Id)
        {
            _logger.LogInformation("Buscando usuario con UserId {UserId}", Id);

            var searchUser = await _userRepository.GetByCondition(u => u.UserId == Id);

            if (searchUser is null)
            {
                _logger.LogWarning("No se encontró ningún usuario con UserId {UserId}", Id);
                throw new NotFoundException("Usuario no encontrado");
            }

            var response = _mapper.Map<UserResponse>(searchUser);

            _logger.LogInformation("Usuario con UserId {UserId} encontrado exitosamente", Id);

            return response;
        }
    }
}
