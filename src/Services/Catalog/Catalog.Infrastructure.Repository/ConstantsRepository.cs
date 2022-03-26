using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repository
{
    public static class ConstantsRepository
    {
        public const string DATABASE_SETTINGS_SECTION = "DatabaseSettings";
        public const string CONNECTION_STRING_SETTINGS_KEY = "ConnectionString";
        public const string DATABASE_NAME_SETTINGS_KEY = "DatabaseName";
        public const string COLLECTION_NAME_SETTINGS_KEY = "ProductsCollectionName";
    }
}
