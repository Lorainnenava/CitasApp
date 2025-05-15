using AutoMapper;
using MyApp.Application.DTOs.Utilitaries.Allergies;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers.Utilitaries
{
    public class AllergyProfile : Profile
    {
        public AllergyProfile()
        {
            CreateMap<AllergyRequest, AllergiesEntity>();
            CreateMap<AllergiesEntity, AllergyResponse>();
        }
    }
}
