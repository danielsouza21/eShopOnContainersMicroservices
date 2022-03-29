using Dapper;
using Discount.Domain.Entities;
using Discount.Infrastructure.Repository.Repositories.DapperQueries;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;

namespace Discount.Infrastructure.Repository.Repositories
{
    public class DapperDiscountRepository : IDiscountRepository
    {
        private readonly string _databaseConnectionString;

        public DapperDiscountRepository(IConfiguration configuration)
        {
            _databaseConnectionString = configuration.GetConnectionString(ConstantsRepository.POSTGRE_CONNECTION_STRING_KEY_CONFIG);
        }

        public async Task<Coupon> GetDiscountAsync(string productName)
        {
            using var connection = new NpgsqlConnection(_databaseConnectionString);

            var populatedQuerySql = string.Format(DiscountSqlQueriesConstants.SELECT_DISCOUNT_QUERY_SQL, productName);
            var couponFound = await connection.QueryFirstOrDefaultAsync<Coupon>(populatedQuerySql);

            if (couponFound is null)
            {
                return new Coupon
                {
                    ProductName = ConstantsRepository.DEFAULT_PRODUCT_NAME_DISCOUNT,
                    Amount = ConstantsRepository.DEFAULT_PRODUCT_AMOUNT_DISCOUNT,
                    Description = ConstantsRepository.DEFAULT_PRODUCT_DESCRIPTION_DISCOUNT
                };
            }

            return couponFound;
        }

        public async Task<bool> CreateDiscountAsync(Coupon coupon)
        {
            var populatedInsertQuerySql = string.Format(
                DiscountSqlQueriesConstants.INSERT_INTO_DISCOUNT_QUERY_SQL,
                coupon.ProductName, coupon.Description, coupon.Amount);

            return await PerformDatabaseOperation(_databaseConnectionString, populatedInsertQuerySql);
        }

        public async Task<bool> UpdateDiscountAsync(Coupon coupon)
        {
            var populatedUpdateQuerySql = string.Format(
                DiscountSqlQueriesConstants.UPDATE_DISCOUNT_QUERY_SQL,
                coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id);

            return await PerformDatabaseOperation(_databaseConnectionString, populatedUpdateQuerySql);
        }

        public async Task<bool> DeleteDiscountAsync(string productName)
        {
            var populatedDeleteQuerySql = string.Format(DiscountSqlQueriesConstants.DELETE_DISCOUNT_QUERY_SQL, productName);
            return await PerformDatabaseOperation(_databaseConnectionString, populatedDeleteQuerySql);
        }

        private static async Task<bool> PerformDatabaseOperation(string connectionString, string querySql)
        {
            using var connection = new NpgsqlConnection(connectionString);

            var affectedRows = await connection.ExecuteAsync(querySql);

            if (affectedRows == 0) return false;
            return true;
        }
    }
}
