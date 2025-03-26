using Application.DTOs.User;
using Application.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Infrastructure.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.User
{
    public class UserGetAllUseCase : IUserGetAllUseCase
    {
        private readonly IUserRepository _userRepository; // Repositorio de usuarios
        private readonly IMapper _mapper; // Servicio de mapeo (AutoMapper).

        // Constructor que inicializa las dependencias.
        public UserGetAllUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserResponse>> Execute()
        {
            try
            {
                var usersSearch = await _userRepository.GetAll();

                var response = _mapper.Map<List<UserResponse>>(usersSearch);

                return response;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ha ocurrido un error al obtener todos los usuarios", ex);
            }
        }
    }
}
