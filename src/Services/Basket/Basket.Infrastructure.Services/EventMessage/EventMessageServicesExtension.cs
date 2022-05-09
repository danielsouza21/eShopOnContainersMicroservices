using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrastructure.Services.EventMessage
{
    public static class EventMessageServicesExtension
    {
        public static void ConfigureMassTransitRabbitMq(this IServiceCollection services, string eventBusHostAddress)
        {
            services.AddMassTransit(config => {
                config.UsingRabbitMq((context, cfg) => {
                    cfg.Host(eventBusHostAddress);
                });
            });
        }
    }
}
