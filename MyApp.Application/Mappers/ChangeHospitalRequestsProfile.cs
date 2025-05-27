using AutoMapper;
using MyApp.Application.DTOs.ChangeHospitalRequests;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers
{
    public class ChangeHospitalRequestsProfile : Profile
    {
        public ChangeHospitalRequestsProfile()
        {
            CreateMap<ChangeHospitalRequestCreateRequest, ChangeHospitalRequestsEntity>();
            CreateMap<ChangeHospitalRequestsEntity, ChangeHospitalRequestResponse>();
        }
    }
}
