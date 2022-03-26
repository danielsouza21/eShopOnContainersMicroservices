using Catalog.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        #region Public Read Methods

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var dataCursor = await _catalogContext.Products.FindAsync(p => true);
            return await dataCursor.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return await GetProductWithFilter(filter);
        }

        public async Task<Product> GetProductByNameAsync(string productName)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Name, productName);
            return await GetProductWithFilter(filter);
        }

        public async Task<Product> GetProductByCategoryAsync(string categoryName)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return await GetProductWithFilter(filter);
        }

        #endregion

        #region Public Write Methods

        public async Task CreateProductAsync(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProductWithIdAsync(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            var updateResult = await _catalogContext.Products.ReplaceOneAsync(filter, product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProductByIdAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var deleteResult = await _catalogContext.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        #endregion

        #region Private Methods

        private async Task<Product> GetProductWithFilter(FilterDefinition<Product> filter)
        {
            var dataCursor = await _catalogContext.Products.FindAsync(filter);
            return await dataCursor.FirstOrDefaultAsync();
        }

        #endregion
    }
}
