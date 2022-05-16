using EventBus.Messages.Common;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
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
                EventBusConsumerConfiguration.ConfigureBusConsumers(config);

                config.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(eventBusHostAddress);
                    EventBusConsumerConfiguration.ConfigureContextConsumers(context, cfg);
                });
            });

            EventBusConsumerConfiguration.ConfigureDependencyInjectionConsumers(services);
        }
    }
}
