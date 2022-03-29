using Basket.Domain.Entities;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Repository
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketForUserNameAsync(string userName);
        Task<ShoppingCart> UpsertBasketAsync(ShoppingCart basket);
        Task DeleteBasketAsync(string userName);
    }
}
