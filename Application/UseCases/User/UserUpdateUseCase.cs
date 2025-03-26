using Application.DTOs.User;
using Application.Interfaces.UseCases;
using AutoMapper;
using Domain.Entities.User;
using Domain.Interfaces.Infrastructure.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.User
{
    public class UserUpdateUseCase : IUserUpdateUseCase
    {
        private readonly IUserRepository _userRepository; // Repositorio de usuarios
        private readonly IMapper _mapper; // Servicio de mapeo (AutoMapper).

        // Constructor que inicializa las dependencias.
        public UserUpdateUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> Execute(int Id, UserRequest request)
        {
            try
            {
                var userMapped = _mapper.Map<UserModel>(request);

                var userUpdate = await _userRepository.Update(u => u.Id == Id, userMapped);

                var response = _mapper.Map<UserResponse>(userUpdate);

                return response;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ha ocurrido un error en la actualización del usuario", ex);
            }
        }
    }
}
