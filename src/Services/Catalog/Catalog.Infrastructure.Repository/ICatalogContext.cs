using Catalog.Domain.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repository
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
