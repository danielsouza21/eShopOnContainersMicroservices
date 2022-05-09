using AutoMapper;
using Basket.Domain.Entities;
using EventBus.Messages.Events;

namespace Basket.API.Mappings
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }

        public static MapperConfiguration InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BasketProfile());
            });

            return config;
        }
    }
}
