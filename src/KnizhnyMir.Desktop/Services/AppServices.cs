using KnizhnyMir.DataAccess;
using KnizhnyMir.DataAccess.ConnectionFactory;
using KnizhnyMir.DataAccess.Repositories;
using KnizhnyMir.DataAccess.Services;

namespace KnizhnyMir.Desktop.Services
{
    /// <summary>
    /// Единая точка создания сервисов работы с базой данных для всего приложения.
    /// Выбор СУБД (MS SQL или SQLite) выполняется автоматически при запуске.
    /// </summary>
    public static class AppServices
    {
        static AppServices()
        {
            var factory = DatabaseConfig.CreateFactory();

            Products = new ProductService(new ProductRepository(factory));
            Orders = new OrderService(new OrderRepository(factory));
            Auth = new AuthService(new UserRepository(factory));
            Lookups = new LookupRepository(factory);
        }

        /// <summary>Сервис работы с товарами.</summary>
        public static ProductService Products { get; }

        /// <summary>Сервис работы с заказами.</summary>
        public static OrderService Orders { get; }

        /// <summary>Сервис авторизации.</summary>
        public static AuthService Auth { get; }

        /// <summary>Репозиторий справочников.</summary>
        public static LookupRepository Lookups { get; }
    }
}
