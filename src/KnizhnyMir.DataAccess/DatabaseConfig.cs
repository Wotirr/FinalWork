using Microsoft.Data.SqlClient;
using KnizhnyMir.DataAccess.ConnectionFactory;

namespace KnizhnyMir.DataAccess
{
    /// <summary>
    /// Глобальные настройки подключения к базе данных приложения «Книжный мир».
    /// Параметры соответствуют учебному серверу MS SQL (псевдоним «mssql»).
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>Адрес сервера MS SQL.</summary>
        public static string Server { get; set; } = "mssql";

        /// <summary>Имя базы данных.</summary>
        public static string Database { get; set; } = "ispp4110";

        /// <summary>Логин для SQL-аутентификации.</summary>
        public static string UserId { get; set; } = "ispp4110";

        /// <summary>Пароль для SQL-аутентификации.</summary>
        public static string Password { get; set; } = "4110";

        /// <summary>Строка подключения к MS SQL Server.</summary>
        public static string ConnectionString
        {
            get
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = Server,
                    InitialCatalog = Database,
                    UserID = UserId,
                    Password = Password,
                    TrustServerCertificate = true,
                    ConnectTimeout = 5
                };
                return builder.ConnectionString;
            }
        }

        /// <summary>Создаёт фабрику подключений к серверу MS SQL.</summary>
        public static IDbConnectionFactory CreateFactory() => new MSSqlConnectionFactory(ConnectionString);

        /// <summary>
        /// Проверяет доступность сервера MS SQL, открывая тестовое подключение.
        /// </summary>
        /// <returns><c>true</c>, если подключение успешно установлено.</returns>
        public static bool TestConnection()
        {
            try
            {
                using var connection = CreateFactory().CreateConnection();
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
