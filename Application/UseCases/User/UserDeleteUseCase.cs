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
    public class UserDeleteUseCase : IUserDeleteUseCase
    {
        private readonly IUserRepository _userRepository; // Repositorio de usuarios

        // Constructor que inicializa las dependencias.
        public UserDeleteUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Execute(int Id)
        {
            try
            {
                var deleteUser = await _userRepository.Delete(u => u.Id == Id);

                return deleteUser;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ha ocurrido un error en la eliminación del usuario", ex);
            }
        }
    }
}
