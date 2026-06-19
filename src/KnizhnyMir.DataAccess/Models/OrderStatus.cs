using System.ComponentModel.DataAnnotations.Schema;

namespace KnizhnyMir.DataAccess.Models
{
    /// <summary>Статус заказа (новый, в обработке, завершён).</summary>
    [Table("OrderStatus")]
    public class OrderStatus
    {
        /// <summary>Идентификатор статуса.</summary>
        public int Id { get; set; }

        /// <summary>Название статуса.</summary>
        public string Name { get; set; } = string.Empty;
    }
}
