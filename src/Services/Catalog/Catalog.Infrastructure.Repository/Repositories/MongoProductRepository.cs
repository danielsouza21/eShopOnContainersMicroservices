using Catalog.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repository.Repositories
{
    public class MongoProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public MongoProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        #region Public Read Methods

        public async Task<IEnumerable<Product>> GetProductsListAsync()
        {
            var dataCursor = await _catalogContext.Products.FindAsync(p => true);
            return await dataCursor?.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            HandleIdParameter(id);

            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return await GetProductWithFilter(filter);
        }

        public async Task<Product> GetProductByNameAsync(string productName)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Name, productName);
            return await GetProductWithFilter(filter);
        }

        public async Task<IEnumerable<Product>> GetProductsListByCategoryAsync(string categoryName)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            var dataCursor = await _catalogContext.Products.FindAsync(filter);
            return await dataCursor?.ToListAsync();
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
            var products = await dataCursor?.FirstOrDefaultAsync();
            return products; 
        }

        private static void HandleIdParameter(string id)
        {
            if (!ObjectId.TryParse(id, out var parsedId))
            {
                throw new ArgumentException($"{id} is not in the expected {nameof(ObjectId)} format.");
            }
        }

        #endregion
    }
}
