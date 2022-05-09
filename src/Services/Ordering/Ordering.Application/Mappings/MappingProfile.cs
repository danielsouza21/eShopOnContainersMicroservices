using AutoMapper;
using Ordering.Application.Dtos;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrdersDto>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommandRequest>().ReverseMap();
            CreateMap<Order, UpdateOrderCommandRequest>().ReverseMap();
        }

        public static MapperConfiguration InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            return config;
        }
    }
}
