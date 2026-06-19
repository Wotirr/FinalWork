using System.Data;
using Microsoft.Data.SqlClient;

namespace KnizhnyMir.DataAccess.ConnectionFactory
{
    /// <summary>
    /// Фабрика подключений к серверу MS SQL Server.
    /// </summary>
    public class MSSqlConnectionFactory(string connectionString) : IDbConnectionFactory
    {
        private readonly string _connectionString = connectionString;

        /// <inheritdoc />
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
