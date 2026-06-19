using System.ComponentModel.DataAnnotations.Schema;

namespace KnizhnyMir.DataAccess.Models
{
    /// <summary>Единица измерения товара (например, «шт.»).</summary>
    [Table("Unit")]
    public class Unit
    {
        /// <summary>Идентификатор единицы измерения.</summary>
        public int Id { get; set; }

        /// <summary>Название единицы измерения.</summary>
        public string Name { get; set; } = string.Empty;
    }
}
