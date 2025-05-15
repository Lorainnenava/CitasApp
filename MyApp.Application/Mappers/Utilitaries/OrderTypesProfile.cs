using AutoMapper;
using MyApp.Application.DTOs.Utilitaries.OrderTypes;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers.Utilitaries
{
    public class OrderTypeProfile : Profile
    {
        public OrderTypeProfile()
        {
            CreateMap<OrderTypeRequest, OrderTypesEntity>();
            CreateMap<OrderTypesEntity, OrderTypeResponse>();
        }
    }
}
