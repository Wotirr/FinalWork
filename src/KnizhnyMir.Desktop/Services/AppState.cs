using System.Collections.ObjectModel;
using KnizhnyMir.DataAccess.Models;
using KnizhnyMir.Desktop.Models;

namespace KnizhnyMir.Desktop.Services
{
    /// <summary>
    /// Глобальное состояние приложения: текущий авторизованный пользователь
    /// и текущий формируемый заказ (корзина). Хранится в одном месте,
    /// чтобы быть доступным из всех окон.
    /// </summary>
    public static class AppState
    {
        /// <summary>Текущий авторизованный пользователь или <c>null</c> для гостя.</summary>
        public static User? CurrentUser { get; set; }

        /// <summary>Текущий заказ (корзина). Все добавления выполняются в один заказ.</summary>
        public static ObservableCollection<CartItem> Cart { get; } = [];

        /// <summary>Признак того, что пользователь является менеджером или администратором.</summary>
        public static bool IsManagerOrAdmin =>
            CurrentUser is not null &&
            (CurrentUser.RoleName == "Администратор" || CurrentUser.RoleName == "Менеджер");

        /// <summary>Добавляет товар в заказ или увеличивает его количество на 1.</summary>
        public static void AddToCart(Product product)
        {
            var existing = Cart.FirstOrDefault(item => item.Product.Article == product.Article);
            if (existing is null)
            {
                Cart.Add(new CartItem(product));
            }
            else
            {
                existing.Quantity++;
            }
        }
    }
}
