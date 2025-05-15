using AutoMapper;
using MyApp.Application.DTOs.Utilitaries.Statuses;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers.Utilitaries
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<StatusRequest, StatusesEntity>();
            CreateMap<StatusesEntity, StatusResponse>();
        }
    }
}
