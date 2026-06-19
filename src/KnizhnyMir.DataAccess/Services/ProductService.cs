using KnizhnyMir.DataAccess.Models;
using KnizhnyMir.DataAccess.Repositories;

namespace KnizhnyMir.DataAccess.Services
{
    /// <summary>
    /// Бизнес-логика работы с товарами: получение данных, поиск,
    /// фильтрация и сортировка. Не зависит от способа отображения.
    /// </summary>
    public class ProductService(ProductRepository productRepository)
    {
        private readonly ProductRepository _productRepository = productRepository;

        /// <summary>Возвращает все товары из базы данных.</summary>
        public IReadOnlyList<Product> GetAll() => _productRepository.GetAll();

        /// <summary>Возвращает общее количество товаров в базе данных.</summary>
        public int GetTotalCount() => _productRepository.GetTotalCount();

        /// <summary>Возвращает товар по артикулу.</summary>
        public Product? GetByArticle(string article) => _productRepository.GetByArticle(article);

        /// <summary>
        /// Применяет к набору товаров поиск, фильтрацию и сортировку согласно
        /// переданным параметрам. Все условия применяются совместно.
        /// </summary>
        public IReadOnlyList<Product> Apply(IEnumerable<Product> source, ProductQuery query)
        {
            IEnumerable<Product> result = source;

            if (!string.IsNullOrWhiteSpace(query.SearchText))
            {
                var text = query.SearchText.Trim();
                result = result.Where(product =>
                    product.Name.Contains(text, StringComparison.OrdinalIgnoreCase));
            }

            if (query.ManufacturerId.HasValue)
            {
                result = result.Where(product => product.ManufacturerId == query.ManufacturerId.Value);
            }

            if (query.MinPrice.HasValue)
            {
                result = result.Where(product => product.Price >= query.MinPrice.Value);
            }

            if (query.MaxPrice.HasValue)
            {
                result = result.Where(product => product.Price <= query.MaxPrice.Value);
            }

            result = query.SortField switch
            {
                ProductSortField.Price => query.SortDescending
                    ? result.OrderByDescending(product => product.Price)
                    : result.OrderBy(product => product.Price),
                _ => query.SortDescending
                    ? result.OrderByDescending(product => product.Name)
                    : result.OrderBy(product => product.Name)
            };

            return result.ToList();
        }
    }
}
