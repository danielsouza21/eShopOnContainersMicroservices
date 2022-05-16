using EventBus.Messages.Common;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.EventBus
{
    public static class EventBusConsumerConfiguration
    {
        public static void ConfigureContextConsumers(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator cfg)
        {
            //Configure the Consumer<EventClass> of queue events
            //Consumer is a handler to process a new event
            cfg.ReceiveEndpoint(EventBusConstants.BASKET_CHECKOUT_QUEUE_NAME, c =>
            {
                c.ConfigureConsumer<BasketCheckoutConsumer>(context);
            });
        }

        public static void ConfigureBusConsumers(IServiceCollectionBusConfigurator config)
        {
            config.AddConsumer<BasketCheckoutConsumer>();
        }

        public static void ConfigureDependencyInjectionConsumers(IServiceCollection services)
        {
            services.AddScoped<BasketCheckoutConsumer>();
        }
    }
}
