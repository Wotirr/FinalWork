using Dapper;
using KnizhnyMir.DataAccess.ConnectionFactory;
using KnizhnyMir.DataAccess.Models;

namespace KnizhnyMir.DataAccess.Repositories
{
    /// <summary>Репозиторий для доступа к пользователям системы.</summary>
    public class UserRepository(IDbConnectionFactory connectionFactory)
    {
        private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

        private const string SelectWithRole = """
            SELECT u.Id, u.FullName, u.Login, u.Password, u.RoleId, r.Name AS RoleName
            FROM Users u
            JOIN Role r ON r.Id = u.RoleId
            """;

        /// <summary>
        /// Ищет пользователя по логину и паролю.
        /// </summary>
        /// <returns>Найденный пользователь или <c>null</c>, если данные неверны.</returns>
        public User? FindByCredentials(string login, string password)
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.QueryFirstOrDefault<User>(
                $"{SelectWithRole} WHERE u.Login = @Login AND u.Password = @Password",
                new { Login = login, Password = password });
        }

        /// <summary>Возвращает пользователя по идентификатору.</summary>
        public User? GetById(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.QueryFirstOrDefault<User>(
                $"{SelectWithRole} WHERE u.Id = @Id", new { Id = id });
        }

        /// <summary>Возвращает список всех пользователей.</summary>
        public IReadOnlyList<User> GetAll()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Query<User>($"{SelectWithRole} ORDER BY u.FullName").ToList();
        }
    }
}
