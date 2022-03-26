using Catalog.Domain.Entities;
using Catalog.Infrastructure.Repository.Context.Seed;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repository.Context
{
    class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public CatalogContext(IConfiguration configuration)
        {
            GetConfigurationValues(configuration, out var connectionString, out var databaseName, out var collectionName);

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            Products = database.GetCollection<Product>(collectionName);
            CatalogContextSeed.SeedData(Products);
        }

        private static void GetConfigurationValues(IConfiguration configuration, out string connectionString, out string databaseName, out string collectionName)
        {
            var repositorySettings = configuration.GetSection(ConstantsRepository.DATABASE_SETTINGS_SECTION);

            connectionString = repositorySettings[ConstantsRepository.CONNECTION_STRING_SETTINGS_KEY];
            databaseName = repositorySettings[ConstantsRepository.DATABASE_NAME_SETTINGS_KEY];
            collectionName = repositorySettings[ConstantsRepository.COLLECTION_NAME_SETTINGS_KEY];
        }
    }
}
