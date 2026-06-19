using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using KnizhnyMir.DataAccess.Models;
using KnizhnyMir.Desktop.Models;
using KnizhnyMir.Desktop.Services;
using Microsoft.Win32;

namespace KnizhnyMir.Desktop
{
    /// <summary>Окно просмотра и оформления текущего заказа.</summary>
    public partial class OrderWindow : Window
    {
        /// <summary>Создаёт окно текущего заказа.</summary>
        public OrderWindow()
        {
            InitializeComponent();
            CartGrid.ItemsSource = AppState.Cart;
            AppState.Cart.CollectionChanged += (_, _) => RefreshUi();

            ClientText.Text = AppState.CurrentUser is null
                ? "Заказ оформляется от имени гостя."
                : $"Клиент: {AppState.CurrentUser.FullName}";

            RefreshUi();
        }

        private void RefreshUi()
        {
            var total = AppState.Cart.Sum(item => item.Sum);
            TotalText.Text = $"Итого: {total:N2} ₽";

            var isEmpty = AppState.Cart.Count == 0;
            EmptyText.Visibility = isEmpty ? Visibility.Visible : Visibility.Collapsed;
            CartGrid.Visibility = isEmpty ? Visibility.Collapsed : Visibility.Visible;
        }

        private void CartGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Пересчёт выполняется после применения нового значения количества.
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (e.Row.Item is CartItem item && item.Quantity <= 0)
                {
                    AppState.Cart.Remove(item);
                }
                RefreshUi();
            }), DispatcherPriority.Background);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { CommandParameter: CartItem item })
            {
                AppState.Cart.Remove(item);
                RefreshUi();
            }
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            if (AppState.Cart.Count == 0)
            {
                MessageBox.Show("Добавьте товары в заказ перед оформлением.", "Заказ пуст",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                var items = AppState.Cart
                    .Select(cartItem => new OrderItem
                    {
                        ProductArticle = cartItem.Product.Article,
                        Quantity = cartItem.Quantity
                    })
                    .ToList();

                var order = AppServices.Orders.CreateOrder(items, AppState.CurrentUser?.Id);
                var talon = BuildTalon(order);

                MessageBox.Show(
                    $"Заказ №{order.Number} успешно оформлен.\nКод для получения: {order.PickupCode}",
                    "Заказ оформлен", MessageBoxButton.OK, MessageBoxImage.Information);

                OfferSaveTalon(order.Number, talon);

                AppState.Cart.Clear();
                RefreshUi();
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Не удалось оформить заказ.\n\n{exception.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string BuildTalon(Order order)
        {
            var builder = new StringBuilder();
            builder.AppendLine("ТАЛОН НА ПОЛУЧЕНИЕ ЗАКАЗА");
            builder.AppendLine("Книжный мир");
            builder.AppendLine(new string('-', 40));
            builder.AppendLine($"Дата заказа:  {order.OrderDate:dd.MM.yyyy}");
            builder.AppendLine($"Номер заказа: {order.Number}");
            builder.AppendLine($"Клиент:       {AppState.CurrentUser?.FullName ?? "Гость"}");
            builder.AppendLine(new string('-', 40));
            builder.AppendLine("Состав заказа:");
            foreach (var item in AppState.Cart)
            {
                builder.AppendLine($"  {item.Product.Name}");
                builder.AppendLine($"    {item.Quantity} x {item.Product.Price:N2} ₽ = {item.Sum:N2} ₽");
            }
            builder.AppendLine(new string('-', 40));
            builder.AppendLine($"Сумма заказа:      {AppState.Cart.Sum(item => item.Sum):N2} ₽");
            builder.AppendLine($"Код для получения: {order.PickupCode}");
            return builder.ToString();
        }

        private void OfferSaveTalon(int orderNumber, string talon)
        {
            var dialog = new SaveFileDialog
            {
                Title = "Сохранение талона заказа",
                Filter = "Текстовый файл (*.txt)|*.txt",
                FileName = $"Талон_заказ_{orderNumber}.txt"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, talon, Encoding.UTF8);
                MessageBox.Show("Талон сохранён.", "Готово",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();
    }
}
