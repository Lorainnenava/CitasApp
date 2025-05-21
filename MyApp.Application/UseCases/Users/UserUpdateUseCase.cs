using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Users;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;

namespace MyApp.Application.UseCases.Users
{
    public class UserUpdateUseCase : IUserUpdateUseCase
    {
        private readonly IGenericRepository<UsersEntity> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserUpdateUseCase> _logger;

        public UserUpdateUseCase(
            IGenericRepository<UsersEntity> userRepository,
            IMapper mapper,
            ILogger<UserUpdateUseCase> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserResponse> Execute(int Id, UserCreateRequest request)
        {
            _logger.LogInformation("Iniciando actualización del usuario con ID {UserId}", Id);

            var userMapped = _mapper.Map<UsersEntity>(request);

            var searchUser = await _userRepository.GetByCondition(x => x.UserId == Id);

            if (searchUser is null)
            {
                _logger.LogWarning("No se encontró ningún usuario con UserId {UserId}", Id);
                throw new NotFoundException("Usuario no encontrado");
            }

            var userUpdate = await _userRepository.Update(u => u.UserId == Id, searchUser, userMapped);

            var response = _mapper.Map<UserResponse>(userUpdate);

            _logger.LogInformation("Usuario con UserId {UserId} actualizado exitosamente", Id);

            return response;
        }
    }
}
