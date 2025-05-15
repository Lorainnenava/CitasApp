using MyApp.Application.DTOs.Users;
using AutoMapper;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers
{
    public class UserSessionsProfile : Profile
    {
        public UserSessionsProfile()
        {
            CreateMap<UserRequest, UserSessionsEntity>();
            CreateMap<UserSessionsEntity, UserResponse>();
        }
    }
}
