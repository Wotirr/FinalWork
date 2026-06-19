using Dapper;
using KnizhnyMir.DataAccess.ConnectionFactory;
using KnizhnyMir.DataAccess.Models;

namespace KnizhnyMir.DataAccess.Repositories
{
    /// <summary>Репозиторий для доступа к товарам в базе данных.</summary>
    public class ProductRepository(IDbConnectionFactory connectionFactory)
    {
        private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

        private const string SelectWithLookups = """
            SELECT p.Article, p.Name, p.Description, p.Author, p.Price, p.Discount,
                   p.StockQuantity, p.Photo, p.UnitId, p.CategoryId, p.ManufacturerId,
                   c.Name AS CategoryName, m.Name AS ManufacturerName, u.Name AS UnitName
            FROM Product p
            JOIN Category c ON c.Id = p.CategoryId
            JOIN Manufacturer m ON m.Id = p.ManufacturerId
            JOIN Unit u ON u.Id = p.UnitId
            """;

        /// <summary>Возвращает все товары вместе со справочными названиями.</summary>
        public IReadOnlyList<Product> GetAll()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Query<Product>($"{SelectWithLookups} ORDER BY p.Name").ToList();
        }

        /// <summary>Возвращает товар по его артикулу или <c>null</c>, если товар не найден.</summary>
        public Product? GetByArticle(string article)
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.QueryFirstOrDefault<Product>(
                $"{SelectWithLookups} WHERE p.Article = @Article",
                new { Article = article });
        }

        /// <summary>Возвращает общее количество товаров в базе данных.</summary>
        public int GetTotalCount()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Product");
        }
    }
}
