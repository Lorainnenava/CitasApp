using AutoMapper;
using MyApp.Application.DTOs.Utilitaries.Municipalities;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers.Utilitaries
{
    public class MunicipalityProfile : Profile
    {
        public MunicipalityProfile()
        {
            CreateMap<MunicipalityRequest, MunicipalitiesEntity>();
            CreateMap<MunicipalitiesEntity, MunicipalityResponse>();
        }
    }
}
