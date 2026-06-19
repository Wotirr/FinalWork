namespace KnizhnyMir.DataAccess.Services
{
    /// <summary>Поле, по которому выполняется сортировка списка товаров.</summary>
    public enum ProductSortField
    {
        /// <summary>Сортировка по наименованию.</summary>
        Name,

        /// <summary>Сортировка по цене.</summary>
        Price
    }

    /// <summary>
    /// Параметры поиска, фильтрации и сортировки списка товаров.
    /// Все параметры применяются совместно.
    /// </summary>
    public class ProductQuery
    {
        /// <summary>Текст для поиска по наименованию товара (без учёта регистра).</summary>
        public string? SearchText { get; set; }

        /// <summary>Идентификатор производителя для фильтрации (<c>null</c> — все производители).</summary>
        public int? ManufacturerId { get; set; }

        /// <summary>Минимальная цена товара.</summary>
        public decimal? MinPrice { get; set; }

        /// <summary>Максимальная цена товара.</summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>Поле сортировки.</summary>
        public ProductSortField SortField { get; set; } = ProductSortField.Name;

        /// <summary>Признак сортировки по убыванию.</summary>
        public bool SortDescending { get; set; }
    }
}
