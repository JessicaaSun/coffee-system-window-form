using System.Data.SqlClient;

namespace CoffeeSystem.Data
{
    public static class Database
    {
        private static readonly string connectionString =
            "Server=localhost\\SQLEXPRESS;Database=coffee_system;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
