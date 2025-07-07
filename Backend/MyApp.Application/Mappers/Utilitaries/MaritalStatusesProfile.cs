using AutoMapper;
using MyApp.Application.DTOs.Utilitaries.MaritalStatuses;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers.Utilitaries
{
    public class MaritalStatusProfile : Profile
    {
        public MaritalStatusProfile()
        {
            CreateMap<MaritalStatusRequest, MaritalStatusesEntity>();
            CreateMap<MaritalStatusesEntity, MaritalStatusResponse>();
        }
    }
}
