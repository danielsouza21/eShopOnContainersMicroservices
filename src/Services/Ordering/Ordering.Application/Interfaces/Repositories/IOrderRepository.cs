using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.Application.Interfaces.Repositories
{
    /// <summary>
    /// Order Repository DAO Contract
    /// </summary>
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<IReadOnlyCollection<Order>> GetOrdersByUserNameAsync(string userName);
    }
}
