using Application.DTOs.User;
using AutoMapper;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequest, UserModel>();
            CreateMap<UserModel, UserResponse>();
        }
    }
}
