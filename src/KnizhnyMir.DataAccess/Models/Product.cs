using System.ComponentModel.DataAnnotations.Schema;

namespace KnizhnyMir.DataAccess.Models
{
    /// <summary>
    /// Товар (книга). Артикул является естественным первичным ключом.
    /// Поля <see cref="CategoryName"/>, <see cref="ManufacturerName"/> и
    /// <see cref="UnitName"/> заполняются при выборке соединением со справочниками.
    /// </summary>
    [Table("Product")]
    public class Product
    {
        /// <summary>Артикул товара (первичный ключ).</summary>
        public string Article { get; set; } = string.Empty;

        /// <summary>Наименование товара.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Описание товара.</summary>
        public string? Description { get; set; }

        /// <summary>Автор книги.</summary>
        public string? Author { get; set; }

        /// <summary>Цена товара за единицу.</summary>
        public decimal Price { get; set; }

        /// <summary>Действующая скидка в процентах.</summary>
        public int Discount { get; set; }

        /// <summary>Количество товара на складе.</summary>
        public int StockQuantity { get; set; }

        /// <summary>Имя файла с фотографией товара.</summary>
        public string? Photo { get; set; }

        /// <summary>Идентификатор единицы измерения.</summary>
        public int UnitId { get; set; }

        /// <summary>Идентификатор категории.</summary>
        public int CategoryId { get; set; }

        /// <summary>Идентификатор производителя.</summary>
        public int ManufacturerId { get; set; }

        /// <summary>Название категории (заполняется при выборке).</summary>
        [NotMapped]
        public string? CategoryName { get; set; }

        /// <summary>Название производителя (заполняется при выборке).</summary>
        [NotMapped]
        public string? ManufacturerName { get; set; }

        /// <summary>Название единицы измерения (заполняется при выборке).</summary>
        [NotMapped]
        public string? UnitName { get; set; }
    }
}
