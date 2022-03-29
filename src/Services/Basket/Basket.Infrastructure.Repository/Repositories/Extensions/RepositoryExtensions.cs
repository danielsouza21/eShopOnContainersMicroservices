using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrastructure.Repository.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static void ConfigureRedisBasket(this IServiceCollection services, IConfiguration configuration)
        {
            var cacheSettingsSection = configuration.GetSection(RepositoryConstants.REDIS_CACHE_SETTINGS_SECTION_CONFIG);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = cacheSettingsSection[RepositoryConstants.REDIS_CONNECTION_STRING_KEY_CONFIG];
            });
        }
    }
}
