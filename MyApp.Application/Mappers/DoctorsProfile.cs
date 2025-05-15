using AutoMapper;
using MyApp.Application.DTOs.Doctors;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<DoctorRequest, DoctorsEntity>();
            CreateMap<DoctorsEntity, DoctorResponse>();
        }
    }
}
