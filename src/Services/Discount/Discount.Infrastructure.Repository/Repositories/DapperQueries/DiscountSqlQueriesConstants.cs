namespace Discount.Infrastructure.Repository.Repositories.DapperQueries
{
    public static class DiscountSqlQueriesConstants
    {
        public const string SELECT_DISCOUNT_QUERY_SQL = "SELECT * FROM Coupon WHERE ProductName = '{0}'";
        public const string INSERT_INTO_DISCOUNT_QUERY_SQL = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('{0}', '{1}', {2})";
        public const string UPDATE_DISCOUNT_QUERY_SQL = "UPDATE Coupon SET ProductName='{0}', Description = '{1}', Amount = {2} WHERE Id = '{3}'";
        public const string DELETE_DISCOUNT_QUERY_SQL = "DELETE FROM Coupon WHERE ProductName = '{0}'";

        public const string DROP_DISCOUNT_TABLE_QUERY_SQL = "DROP TABLE IF EXISTS Coupon";
        public const string CREATE_DISCOUNT_TABLE_QUERY_SQL = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, ProductName VARCHAR(24) NOT NULL,Description TEXT,Amount INT)";
    }
}
