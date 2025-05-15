using AutoMapper;
using MyApp.Application.DTOs.Utilitaries.BloodTypes;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers.Utilitaries
{
    public class BloodTypeProfile : Profile
    {
        public BloodTypeProfile()
        {
            CreateMap<BloodTypeRequest, BloodTypesEntity>();
            CreateMap<BloodTypesEntity, BloodTypeResponse>();
        }
    }
}
