using AutoMapper;
using MyApp.Application.DTOs.Utilitaries.Diseases;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers.Utilitaries
{
    public class DiseaseProfile : Profile
    {
        public DiseaseProfile()
        {
            CreateMap<DiseaseRequest, DiseasesEntity>();
            CreateMap<DiseasesEntity, DiseaseResponse>();
        }
    }
}
