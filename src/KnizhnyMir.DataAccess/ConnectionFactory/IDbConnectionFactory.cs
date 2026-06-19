using System.Data;

namespace KnizhnyMir.DataAccess.ConnectionFactory
{
    /// <summary>
    /// Фабрика подключений к базе данных. Скрывает от вызывающего кода
    /// конкретную СУБД (MS SQL Server или SQLite).
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>Создаёт новое подключение к базе данных.</summary>
        IDbConnection CreateConnection();
    }
}
