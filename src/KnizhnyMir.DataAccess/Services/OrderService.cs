using KnizhnyMir.DataAccess.Models;
using KnizhnyMir.DataAccess.Repositories;

namespace KnizhnyMir.DataAccess.Services
{
    /// <summary>Бизнес-логика оформления и обработки заказов.</summary>
    public class OrderService(OrderRepository orderRepository)
    {
        /// <summary>Идентификатор статуса «Новый».</summary>
        public const int StatusNew = 1;

        private readonly OrderRepository _orderRepository = orderRepository;
        private static readonly Random Random = new();

        /// <summary>
        /// Формирует и сохраняет новый заказ из переданных позиций.
        /// Заказу присваивается следующий номер, статус «Новый», текущая дата
        /// и случайный трёхзначный код получения.
        /// </summary>
        /// <param name="items">Позиции заказа (артикул и количество).</param>
        /// <param name="userId">Клиент, оформивший заказ; <c>null</c> для гостя.</param>
        /// <returns>Сохранённый заказ с присвоенным номером и кодом получения.</returns>
        public Order CreateOrder(IEnumerable<OrderItem> items, int? userId)
        {
            var order = new Order
            {
                Number = _orderRepository.GetNextNumber(),
                OrderDate = DateTime.Now.Date,
                DeliveryDate = null,
                PickupCode = GeneratePickupCode(),
                StatusId = StatusNew,
                UserId = userId,
                Items = items.ToList()
            };

            _orderRepository.Create(order);
            return order;
        }

        /// <summary>Генерирует случайный трёхзначный код получения заказа.</summary>
        public static string GeneratePickupCode() => Random.Next(0, 1000).ToString("D3");

        /// <summary>Возвращает заказ по номеру вместе с его позициями.</summary>
        public Order? GetOrder(int number) => _orderRepository.GetByNumber(number);

        /// <summary>Возвращает список всех заказов.</summary>
        public IReadOnlyList<Order> GetAllOrders() => _orderRepository.GetAll();

        /// <summary>Обновляет дату доставки и статус заказа.</summary>
        public void UpdateDeliveryAndStatus(int number, DateTime? deliveryDate, int statusId) =>
            _orderRepository.UpdateDeliveryAndStatus(number, deliveryDate, statusId);
    }
}
