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
    public class UserGetByIdUseCase: IUserGetByIdUseCase
    {
        private readonly IUserRepository _userRepository; // Repositorio de usuarios
        private readonly IMapper _mapper; // Servicio de mapeo (AutoMapper).

        // Constructor que inicializa las dependencias.
        public UserGetByIdUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> Execute(int Id)
        {
            try
            {
                var searchUser = await _userRepository.GetByCondition(u => u.Id == Id);

                var response= _mapper.Map<UserResponse>(searchUser);

                return response;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ha ocurrido un error al obtener un usuario", ex);
            }
        }
    }
}
