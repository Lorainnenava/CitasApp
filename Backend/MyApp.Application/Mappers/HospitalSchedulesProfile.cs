using AutoMapper;
using MyApp.Application.DTOs.HospitalSchedules;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers
{
    public class HospitalSchedulesProfile : Profile
    {
        public HospitalSchedulesProfile()
        {
            CreateMap<HospitalScheduleRequest, HospitalSchedulesEntity>();
            CreateMap<HospitalSchedulesEntity, HospitalScheduleResponse>();
        }
    }
}
