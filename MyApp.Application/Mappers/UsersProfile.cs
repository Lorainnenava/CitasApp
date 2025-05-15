using MyApp.Application.DTOs.Users;
using AutoMapper;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<UserRequest, UsersEntity>();
            CreateMap<UsersEntity, UserResponse>();
        }
    }
}
