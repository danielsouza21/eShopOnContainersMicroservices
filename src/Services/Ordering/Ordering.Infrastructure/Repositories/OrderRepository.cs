using Microsoft.EntityFrameworkCore;
using Ordering.Application.Interfaces.Repositories;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order, OrderContext>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Order>> GetOrdersByUserNameAsync(string userName)
        {
            var orderList = await _dbContext.Orders.Where(order => order.UserName == userName).ToListAsync();
            return orderList;
        }
    }
}
