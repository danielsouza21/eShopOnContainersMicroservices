using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrastructure.Repository.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static void ConfigureRedisBasket(this IServiceCollection services, string cacheSettingsConnectionString)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = cacheSettingsConnectionString;
            });
        }
    }
}
