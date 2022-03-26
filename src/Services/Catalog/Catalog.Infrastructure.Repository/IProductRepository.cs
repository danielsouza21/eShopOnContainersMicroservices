using Catalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsListAsync();
        Task<Product> GetProductByIdAsync(string id);
        Task<Product> GetProductByNameAsync(string productName);
        Task<IEnumerable<Product>> GetProductsListByCategoryAsync(string categoryName);

        Task CreateProductAsync(Product product);
        Task<bool> UpdateProductWithIdAsync(Product product);
        Task<bool> DeleteProductByIdAsync(string id);
    }
}
