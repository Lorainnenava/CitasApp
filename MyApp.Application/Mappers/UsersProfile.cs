using AutoMapper;
using MyApp.Application.DTOs.Users;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<UserCreateRequest, UsersEntity>();
            CreateMap<UserUpdateRequest, UsersEntity>();
            CreateMap<UsersEntity, UserResponse>();
        }
    }
}
