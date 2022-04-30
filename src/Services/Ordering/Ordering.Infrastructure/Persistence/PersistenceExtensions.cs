using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Interfaces.Repositories;
using Ordering.Infrastructure.Persistence.Contexts;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure.Persistence
{
    public static class PersistenceExtensions
    {
        private const string ORDER_REPOSITORY_CONNECTION_STRING_KEY = "OrderingServer";

        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var orderConnectionString = configuration.GetConnectionString(ORDER_REPOSITORY_CONNECTION_STRING_KEY);
            services.AddDbContext<OrderContext>(options => options.UseSqlServer(orderConnectionString));

            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
