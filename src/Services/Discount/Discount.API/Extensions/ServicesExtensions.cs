using Discount.Infrastructure.Repository;
using Discount.Infrastructure.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Discount.API.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddDependenciesInjection(this IServiceCollection services)
        {
            services.AddScoped<IDiscountRepository, DapperDiscountRepository>();
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
