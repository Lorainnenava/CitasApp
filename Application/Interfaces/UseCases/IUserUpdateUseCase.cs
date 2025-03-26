using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases
{
    public interface IUserUpdateUseCase
    {
        Task<UserResponse> Execute(int Id, UserRequest request);
    }
}
