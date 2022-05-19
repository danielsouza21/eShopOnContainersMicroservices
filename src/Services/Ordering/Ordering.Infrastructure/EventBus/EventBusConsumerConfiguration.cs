using EventBus.Messages.Common;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.EventBus
{
    public static class EventBusConsumerConfiguration
    {
        public static void AddBusConsumers(this IServiceCollectionBusConfigurator config)
        {
            config.AddConsumer<BasketCheckoutConsumer>();
        }

        public static void ConfigureContextConsumers(this IRabbitMqBusFactoryConfigurator cfg, IBusRegistrationContext context)
        {
            //Configure the Consumer<EventClass> of queue events
            //Consumer is a handler to process a new event
            cfg.ReceiveEndpoint(EventBusConstants.BASKET_CHECKOUT_QUEUE_NAME, c =>
            {
                c.ConfigureConsumer<BasketCheckoutConsumer>(context);
            });
        }

        public static void ConfigureInjectionEventBusConsumers(this IServiceCollection services)
        {
            services.AddScoped<BasketCheckoutConsumer>();
        }
    }
}
