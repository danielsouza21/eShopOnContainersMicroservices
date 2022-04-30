using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence.Contexts
{
    public class OrderContextSeed
    {
        public async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredSeedOrders());
                await orderContext.SaveChangesAsync();

                logger.LogInformation($"Seed database associated with context {typeof(OrderContext).Name}");
            }
        }

        private static IEnumerable<Order> GetPreconfiguredSeedOrders()
        {
            return new List<Order>
            {
                new Order() {
                    UserName = "danielsouza21", 
                    FirstName = "Daniel", 
                    LastName = "Souza", 
                    EmailAddress = "danielsouza21tst@gmail.com", 
                    AddressLine = "BeloHorizonte, MG", 
                    Country = "Brasil", 
                    TotalPrice = 350 
                }
            };
        }
    }
}
