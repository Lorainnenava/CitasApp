using AutoMapper;
using MyApp.Application.DTOs.DoctorSchedules;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers
{
    public class DoctorScheduleProfile : Profile
    {
        public DoctorScheduleProfile()
        {
            CreateMap<DoctorScheduleRequest, DoctorSchedulesEntity>();
            CreateMap<DoctorSchedulesEntity, DoctorScheduleResponse>();
        }
    }
}
