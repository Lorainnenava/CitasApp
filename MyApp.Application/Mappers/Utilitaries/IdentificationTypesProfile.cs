using AutoMapper;
using MyApp.Application.DTOs.Utilitaries.IdentificationTypes;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers.Utilitaries
{
    public class IdentificationTypeProfile : Profile
    {
        public IdentificationTypeProfile()
        {
            CreateMap<IdentificationTypeRequest, IdentificationTypesEntity>();
            CreateMap<IdentificationTypesEntity, IdentificationTypeResponse>();
        }
    }
}
