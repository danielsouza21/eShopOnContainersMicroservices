using Basket.Infrastructure.Repository;
using Basket.Infrastructure.Repository.Repositories;
using Basket.Infrastructure.Repository.Repositories.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Basket.API.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddDependenciesInjectionAndServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureRedisBasket(configuration);
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
