using System.ComponentModel.DataAnnotations.Schema;

namespace KnizhnyMir.DataAccess.Models
{
    /// <summary>Категория товара (художественная литература, хрестоматия и т.д.).</summary>
    [Table("Category")]
    public class Category
    {
        /// <summary>Идентификатор категории.</summary>
        public int Id { get; set; }

        /// <summary>Название категории.</summary>
        public string Name { get; set; } = string.Empty;
    }
}
