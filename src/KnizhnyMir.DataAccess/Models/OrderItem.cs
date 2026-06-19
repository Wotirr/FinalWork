using System.ComponentModel.DataAnnotations.Schema;

namespace KnizhnyMir.DataAccess.Models
{
    /// <summary>Позиция заказа: товар и его количество в конкретном заказе.</summary>
    [Table("OrderItem")]
    public class OrderItem
    {
        /// <summary>Идентификатор позиции заказа.</summary>
        public int Id { get; set; }

        /// <summary>Номер заказа, которому принадлежит позиция.</summary>
        public int OrderNumber { get; set; }

        /// <summary>Артикул заказанного товара.</summary>
        public string ProductArticle { get; set; } = string.Empty;

        /// <summary>Количество единиц товара.</summary>
        public int Quantity { get; set; }

        /// <summary>Наименование товара (заполняется при выборке).</summary>
        [NotMapped]
        public string? ProductName { get; set; }

        /// <summary>Цена товара за единицу с учётом скидки (заполняется при выборке).</summary>
        [NotMapped]
        public decimal Price { get; set; }

        /// <summary>Стоимость позиции (цена, умноженная на количество).</summary>
        [NotMapped]
        public decimal Sum => Price * Quantity;
    }
}
