using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs.Users;
using MyApp.Application.Interfaces.UseCases.Users;
using MyApp.Application.Validators.Users;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces.Infrastructure;
using MyApp.Shared.Exceptions;
using MyApp.Shared.Services;

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

        public async Task<UserResponse> Execute(int UserId, UserUpdateRequest request)
        {
            _logger.LogInformation("Iniciando actualización del usuario con UserId: {UserId}", UserId);

            var validator = new UserUpdateValidator();
            ValidatorHelper.ValidateAndThrow(request, validator);

            var userMapped = _mapper.Map<UsersEntity>(request);
            userMapped.UpdatedAt = DateTime.Now;

            UsersEntity? searchUser = await _userRepository.GetByCondition(x => x.UserId == UserId);

            if (searchUser is null)
            {
                _logger.LogWarning("No se encontró ningún usuario con UserId: {UserId}", UserId);
                throw new NotFoundException("Usuario no encontrado.");
            }

            var userUpdate = await _userRepository.Update(searchUser, userMapped);

            var response = _mapper.Map<UserResponse>(userUpdate);

            _logger.LogInformation("Usuario con UserId: {UserId} actualizado exitosamente", UserId);

            return response;
        }
    }
}
