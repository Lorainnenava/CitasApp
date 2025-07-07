using AutoMapper;
using MyApp.Application.DTOs.Utilitaries.StatusTypes;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers.Utilitaries
{
    public class StatusTypeProfile : Profile
    {
        public StatusTypeProfile()
        {
            CreateMap<StatusTypeRequest, StatusTypesEntity>();
            CreateMap<StatusTypesEntity, StatusTypeResponse>();
        }
    }
}
