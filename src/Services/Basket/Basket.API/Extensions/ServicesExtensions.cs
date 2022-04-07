using Basket.Infrastructure.Repository;
using Basket.Infrastructure.Repository.Repositories;
using Basket.Infrastructure.Repository.Repositories.Extensions;
using Basket.Infrastructure.Services;
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
                .GetSection(AppConstants.GRPC_SETTINGS_CONFIG_KEY)[AppConstants.DISCOUNT_URL_GRPC_CONFIG_KEY];

            //External Services
            services.ConfigureRedisBasket(cacheSettingsConnectionString);
            services.ConfigureDiscountGrpcService(gprcDiscountUrl);

            //Repositories
            services.AddScoped<IBasketRepository, RedisBasketRepository>();
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
