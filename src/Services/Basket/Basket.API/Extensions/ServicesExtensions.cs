using Basket.API.Mappings;
using Basket.Infrastructure.Repository;
using Basket.Infrastructure.Repository.Repositories;
using Basket.Infrastructure.Repository.Repositories.Extensions;
using Basket.Infrastructure.Services.EventMessage;
using Basket.Infrastructure.Services.GrpcService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Basket.API.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddDependenciesInjectionAndServices(this IServiceCollection services, IConfiguration configuration)
        {
            var cacheSettingsConnectionString = configuration
                .GetSection(AppConstants.REDIS_CACHE_SETTINGS_SECTION_CONFIG)[AppConstants.REDIS_CONNECTION_STRING_KEY_CONFIG];

            var gprcDiscountUrl = configuration
                .GetSection(AppConstants.GRPC_SETTINGS_SECTION_CONFIG)[AppConstants.DISCOUNT_URL_GRPC_CONFIG_KEY_CONFIG];

            var eventBusHostAddress = configuration
                .GetSection(AppConstants.MESSAGE_BUS_SETTINGS_SECTION_CONFIG)[AppConstants.MESSAGE_BUS_HOSTADDRESS_KEY_CONFIG]; 

            //External Services
            services.ConfigureRedisBasket(cacheSettingsConnectionString);
            services.ConfigureDiscountGrpcService(gprcDiscountUrl);

            //Message Exchange
            services.ConfigureMassTransitRabbitMq(eventBusHostAddress);

            //Repositories
            services.AddScoped<IBasketRepository, RedisBasketRepository>();

            //AutoMapper
            var mapper = BasketProfile.InitializeAutoMapper().CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(AppConstants.SWAGGER_APP_VERSION, new OpenApiInfo
                {
                    Title = AppConstants.SWAGGER_APP_NAME,
                    Version = AppConstants.SWAGGER_APP_VERSION
                });
            });
        }
    }
}
