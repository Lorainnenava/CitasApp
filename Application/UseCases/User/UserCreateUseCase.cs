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
    public class UserCreateUseCase : IUserCreateUseCase
    {
        private readonly IUserRepository _userRepository; // Repositorio de usuarios
        private readonly IMapper _mapper; // Servicio de mapeo (AutoMapper).

        // Constructor que inicializa las dependencias.
        public UserCreateUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Maneja la creación de un usuario.
        /// </summary>
        /// <param name="request">DTO con los datos del usuario a crear.</param>
        /// <returns>El DTO con los datos del usuario creado.</returns>
        public async Task<UserResponse> Execute(UserRequest request)
        {
            try
            {
                // Mapear el DTO a la entidad
                var entityMapped = _mapper.Map<UserModel>(request);

                // Llamar al repositorio para crear la entidad
                var userCreated = await _userRepository.Create(entityMapped);

                // Mapear la entidad a la respuesta (DTO)
                var response = _mapper.Map<UserResponse>(userCreated);

                return response;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ha ocurrido un error en la creación del usuario", ex);
            }
        }
    }
}
