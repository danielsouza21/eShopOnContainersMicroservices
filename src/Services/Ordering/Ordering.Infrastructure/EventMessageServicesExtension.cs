using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.EventBus;

namespace Ordering.Infrastructure
{
    public static class EventMessageServicesExtension
    {
        public static void ConfigureMassTransitRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var eventBusHostAddress = configuration
                .GetSection(InfrastructureConstants.MESSAGE_BUS_SETTINGS_SECTION_CONFIG)[InfrastructureConstants.MESSAGE_BUS_HOSTADDRESS_KEY_CONFIG];

            services.AddMassTransit(config =>
            {
                config.AddBusConsumers();

                config.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(eventBusHostAddress);
                    cfg.ConfigureContextConsumers(context);
                });
            });
            services.AddMassTransitHostedService();

            services.ConfigureInjectionEventBusConsumers();
        }
    }
}
