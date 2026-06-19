using System.ComponentModel.DataAnnotations.Schema;

namespace KnizhnyMir.DataAccess.Models
{
    /// <summary>
    /// Заказ. Номер заказа является первичным ключом. Заказ может быть оформлен
    /// гостем (тогда <see cref="UserId"/> не заполнен) или авторизованным клиентом.
    /// </summary>
    [Table("Orders")]
    public class Order
    {
        /// <summary>Номер заказа (первичный ключ).</summary>
        public int Number { get; set; }

        /// <summary>Дата оформления заказа.</summary>
        public DateTime OrderDate { get; set; }

        /// <summary>Дата доставки заказа (может отсутствовать).</summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>Трёхзначный код для получения заказа.</summary>
        public string PickupCode { get; set; } = string.Empty;

        /// <summary>Идентификатор статуса заказа.</summary>
        public int StatusId { get; set; }

        /// <summary>Идентификатор клиента, оформившего заказ (для гостя не заполнен).</summary>
        public int? UserId { get; set; }

        /// <summary>Название статуса заказа (заполняется при выборке).</summary>
        [NotMapped]
        public string? StatusName { get; set; }

        /// <summary>ФИО клиента, оформившего заказ (заполняется при выборке).</summary>
        [NotMapped]
        public string? ClientName { get; set; }

        /// <summary>Позиции заказа (заполняются при выборке).</summary>
        [NotMapped]
        public List<OrderItem> Items { get; set; } = [];

        /// <summary>Итоговая сумма заказа.</summary>
        [NotMapped]
        public decimal TotalSum => Items.Sum(item => item.Sum);
    }
}
