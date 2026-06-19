using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using KnizhnyMir.DataAccess.Models;
using KnizhnyMir.DataAccess.Services;
using KnizhnyMir.Desktop.Services;

namespace KnizhnyMir.Desktop
{
    /// <summary>Главное окно: каталог товаров с поиском, фильтрацией и сортировкой.</summary>
    public partial class MainWindow : Window
    {
        private IReadOnlyList<Product>? _allProducts;
        private int _totalCount;

        /// <summary>Создаёт главное окно и загружает данные каталога.</summary>
        public MainWindow()
        {
            InitializeComponent();
            RestoreCurrentUser();
            LoadData();
        }

        private void RestoreCurrentUser()
        {
            var saved = AppSettings.LoadCurrentUser();
            if (saved is not null)
            {
                AppState.CurrentUser = new User
                {
                    Id = saved.UserId,
                    FullName = saved.FullName,
                    RoleName = saved.RoleName
                };
            }
        }

        private void LoadData()
        {
            try
            {
                _allProducts = AppServices.Products.GetAll();
                _totalCount = AppServices.Products.GetTotalCount();

                var manufacturers = new List<Manufacturer> { new() { Id = 0, Name = "Все производители" } };
                manufacturers.AddRange(AppServices.Lookups.GetManufacturers());
                ManufacturerBox.ItemsSource = manufacturers;
                ManufacturerBox.SelectedIndex = 0;

                ApplyFilters();
                UpdateUserInterface();
                UpdateCartButton();
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    $"Не удалось загрузить данные из базы.\n\n{exception.Message}",
                    "Ошибка подключения к базе данных",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ApplyFilters()
        {
            if (_allProducts is null)
            {
                return;
            }

            var query = new ProductQuery
            {
                SearchText = SearchBox.Text,
                ManufacturerId = ManufacturerBox.SelectedItem is Manufacturer manufacturer && manufacturer.Id > 0
                    ? manufacturer.Id
                    : null,
                MinPrice = ParsePrice(MinPriceBox.Text),
                MaxPrice = ParsePrice(MaxPriceBox.Text),
                SortField = SortFieldBox.SelectedIndex == 1 ? ProductSortField.Price : ProductSortField.Name,
                SortDescending = SortOrderBox.SelectedIndex == 1
            };

            var filtered = AppServices.Products.Apply(_allProducts, query);
            ProductsList.ItemsSource = filtered;
            CountText.Text = $"Показано {filtered.Count} из {_totalCount}";
            EmptyMessage.Visibility = filtered.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private static decimal? ParsePrice(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            var normalized = text.Trim().Replace(',', '.');
            return decimal.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out var value) && value >= 0
                ? value
                : null;
        }

        private void Filter_Changed(object sender, RoutedEventArgs e) => ApplyFilters();

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { CommandParameter: Product product })
            {
                AppState.AddToCart(product);
                UpdateCartButton();
            }
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            var orderWindow = new OrderWindow { Owner = this };
            orderWindow.ShowDialog();
            UpdateCartButton();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow { Owner = this };
            if (loginWindow.ShowDialog() == true)
            {
                UpdateUserInterface();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AppState.CurrentUser = null;
            AppSettings.ClearCurrentUser();
            UpdateUserInterface();
        }

        private void ManageOrders_Click(object sender, RoutedEventArgs e)
        {
            var ordersWindow = new OrdersManagementWindow { Owner = this };
            ordersWindow.ShowDialog();
        }

        private void UpdateUserInterface()
        {
            if (AppState.CurrentUser is null)
            {
                UserNameText.Text = string.Empty;
                LoginButton.Visibility = Visibility.Visible;
                LogoutButton.Visibility = Visibility.Collapsed;
                ManageOrdersButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                UserNameText.Text = AppState.CurrentUser.FullName;
                LoginButton.Visibility = Visibility.Collapsed;
                LogoutButton.Visibility = Visibility.Visible;
                ManageOrdersButton.Visibility = AppState.IsManagerOrAdmin ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void UpdateCartButton()
        {
            var totalItems = AppState.Cart.Sum(item => item.Quantity);
            CartButton.Visibility = AppState.Cart.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            CartButton.Content = $"Корзина ({totalItems})";
        }
    }
}
