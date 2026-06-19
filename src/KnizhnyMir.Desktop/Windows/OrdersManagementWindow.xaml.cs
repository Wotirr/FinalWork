using System.Windows;
using KnizhnyMir.DataAccess.Models;
using KnizhnyMir.Desktop.Services;

namespace KnizhnyMir.Desktop
{
    /// <summary>Окно работы с заказами для менеджера и администратора.</summary>
    public partial class OrdersManagementWindow : Window
    {
        private int? _currentOrderNumber;

        /// <summary>Создаёт окно работы с заказами.</summary>
        public OrdersManagementWindow()
        {
            InitializeComponent();
            StatusBox.ItemsSource = AppServices.Lookups.GetOrderStatuses();
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(SearchNumberBox.Text.Trim(), out var number))
            {
                MessageBox.Show("Введите числовой номер заказа.", "Некорректный номер",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var order = AppServices.Orders.GetOrder(number);
            if (order is null)
            {
                DetailsPanel.Visibility = Visibility.Collapsed;
                MessageBox.Show($"Заказ №{number} не найден.", "Заказ не найден",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _currentOrderNumber = order.Number;
            OrderTitle.Text = $"Заказ №{order.Number}";
            OrderDateText.Text = order.OrderDate.ToString("dd.MM.yyyy");
            OrderClientText.Text = string.IsNullOrEmpty(order.ClientName) ? "Гость" : order.ClientName;
            DeliveryDatePicker.SelectedDate = order.DeliveryDate;
            StatusBox.SelectedItem = (StatusBox.ItemsSource as IEnumerable<OrderStatus>)?
                .FirstOrDefault(status => status.Id == order.StatusId);

            DetailsPanel.Visibility = Visibility.Visible;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_currentOrderNumber is null)
            {
                return;
            }

            if (StatusBox.SelectedItem is not OrderStatus status)
            {
                MessageBox.Show("Выберите статус заказа.", "Не выбран статус",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                AppServices.Orders.UpdateDeliveryAndStatus(
                    _currentOrderNumber.Value, DeliveryDatePicker.SelectedDate, status.Id);

                MessageBox.Show("Изменения сохранены.", "Готово",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Не удалось сохранить изменения.\n\n{exception.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
