using Discount.Infrastructure.Repository.Repositories.DapperQueries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;
using System;

namespace Discount.Infrastructure.Repository.Repositories.MigrationExtensions
{
    public static class RepositoryExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migrating postresql database.");

                    var retry = Policy.Handle<NpgsqlException>()
                            .WaitAndRetry(
                                retryCount: 5,
                                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                onRetry: (exception, retryCount, context) =>
                                {
                                    logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                                });

                    //if the postgresql server container is not created on run docker compose this
                    //migration can't fail for network related exception. The retry options for database operations
                    //apply to transient exceptions                    
                    retry.Execute(() => ExecuteMigrations(configuration));

                    logger.LogInformation("Migrated postresql database.");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the postresql database");
                }
            }

            return host;
        }

        private static void ExecuteMigrations(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConstantsRepository.POSTGRE_CONNECTION_STRING_KEY_CONFIG);
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using var command = new NpgsqlCommand
            {
                Connection = connection
            };

            command.CommandText = DiscountSqlQueriesConstants.DROP_DISCOUNT_TABLE_QUERY_SQL;
            command.ExecuteNonQuery();

            command.CommandText = DiscountSqlQueriesConstants.CREATE_DISCOUNT_TABLE_QUERY_SQL;
            command.ExecuteNonQuery();

            command.CommandText = string.Format(
                DiscountSqlQueriesConstants.INSERT_INTO_DISCOUNT_QUERY_SQL,
                "IPhone X", "IPhone Discount", 150);
            command.ExecuteNonQuery();

            command.CommandText = string.Format(
                DiscountSqlQueriesConstants.INSERT_INTO_DISCOUNT_QUERY_SQL,
                "Samsung 10", "Samsung Discount", 100);
            command.ExecuteNonQuery();
        }
    }
}
