using AutoMapper;
using MyApp.Application.DTOs.Utilitaries.PaymentTypes;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappers.Utilitaries
{
    public class PaymentTypeProfile : Profile
    {
        public PaymentTypeProfile()
        {
            CreateMap<PaymentTypeRequest, PaymentTypesEntity>();
            CreateMap<PaymentTypesEntity, PaymentTypeResponse>();
        }
    }
}
