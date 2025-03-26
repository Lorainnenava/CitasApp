using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases
{
    public interface IUserCreateUseCase
    {
        Task<UserResponse> Execute(UserRequest request);
    }
}
