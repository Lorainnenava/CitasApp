using AutoMapper;
using MyApp.Application.DTOs.Utilitaries.Genders;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers.Utilitaries
{
    public class GenderProfile : Profile
    {
        public GenderProfile()
        {
            CreateMap<GenderRequest, GendersEntity>();
            CreateMap<GendersEntity, GenderResponse>();
        }
    }
}
