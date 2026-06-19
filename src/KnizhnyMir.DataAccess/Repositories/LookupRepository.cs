using Dapper;
using KnizhnyMir.DataAccess.ConnectionFactory;
using KnizhnyMir.DataAccess.Models;

namespace KnizhnyMir.DataAccess.Repositories
{
    /// <summary>Репозиторий для доступа к справочникам (категории, производители и т.д.).</summary>
    public class LookupRepository(IDbConnectionFactory connectionFactory)
    {
        private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

        /// <summary>Возвращает список всех категорий.</summary>
        public IReadOnlyList<Category> GetCategories()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Query<Category>("SELECT Id, Name FROM Category ORDER BY Name").ToList();
        }

        /// <summary>Возвращает список всех производителей.</summary>
        public IReadOnlyList<Manufacturer> GetManufacturers()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Query<Manufacturer>("SELECT Id, Name FROM Manufacturer ORDER BY Name").ToList();
        }

        /// <summary>Возвращает список всех единиц измерения.</summary>
        public IReadOnlyList<Unit> GetUnits()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Query<Unit>("SELECT Id, Name FROM Unit ORDER BY Name").ToList();
        }

        /// <summary>Возвращает список всех статусов заказа.</summary>
        public IReadOnlyList<OrderStatus> GetOrderStatuses()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Query<OrderStatus>("SELECT Id, Name FROM OrderStatus ORDER BY Id").ToList();
        }

        /// <summary>Возвращает список всех ролей пользователей.</summary>
        public IReadOnlyList<Role> GetRoles()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Query<Role>("SELECT Id, Name FROM Role ORDER BY Id").ToList();
        }
    }
}
