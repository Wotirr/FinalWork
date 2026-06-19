using System.ComponentModel;
using KnizhnyMir.DataAccess.Models;

namespace KnizhnyMir.Desktop.Models
{
    /// <summary>Позиция корзины (текущего заказа): товар и его количество.</summary>
    public class CartItem(Product product) : INotifyPropertyChanged
    {
        private int _quantity = 1;

        /// <summary>Товар.</summary>
        public Product Product { get; } = product;

        /// <summary>Количество единиц товара в заказе.</summary>
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity == value)
                {
                    return;
                }
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(Sum));
            }
        }

        /// <summary>Стоимость позиции с учётом количества.</summary>
        public decimal Sum => Product.Price * Quantity;

        /// <inheritdoc />
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
