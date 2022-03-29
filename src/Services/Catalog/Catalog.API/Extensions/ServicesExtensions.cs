using Catalog.Infrastructure.Repository;
using Catalog.Infrastructure.Repository.Context;
using Catalog.Infrastructure.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Catalog.API.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddDependenciesInjection(this IServiceCollection services)
        {
            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRepository, MongoProductRepository>();
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
