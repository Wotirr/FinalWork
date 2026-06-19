using KnizhnyMir.Api.Dtos;
using KnizhnyMir.DataAccess.Models;
using KnizhnyMir.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace KnizhnyMir.Api.Controllers
{
    /// <summary>API для получения и редактирования заказов.</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(OrderService orderService) : ControllerBase
    {
        private readonly OrderService _orderService = orderService;

        /// <summary>Возвращает список всех заказов.</summary>
        [HttpGet]
        public ActionResult<IReadOnlyList<Order>> GetAll() => Ok(_orderService.GetAllOrders());

        /// <summary>Возвращает заказ по номеру.</summary>
        [HttpGet("{number:int}")]
        public ActionResult<Order> GetByNumber(int number)
        {
            var order = _orderService.GetOrder(number);
            return order is null ? NotFound() : Ok(order);
        }

        /// <summary>Создаёт новый заказ.</summary>
        [HttpPost]
        public ActionResult<Order> Create(CreateOrderRequest request)
        {
            if (request.Items is null || request.Items.Count == 0)
            {
                return BadRequest("Заказ должен содержать хотя бы одну позицию.");
            }

            var items = request.Items
                .Select(item => new OrderItem { ProductArticle = item.ProductArticle, Quantity = item.Quantity })
                .ToList();

            var order = _orderService.CreateOrder(items, request.UserId);
            return CreatedAtAction(nameof(GetByNumber), new { number = order.Number }, order);
        }

        /// <summary>Обновляет дату доставки и статус заказа.</summary>
        [HttpPut("{number:int}")]
        public IActionResult Update(int number, UpdateOrderRequest request)
        {
            if (_orderService.GetOrder(number) is null)
            {
                return NotFound();
            }

            _orderService.UpdateDeliveryAndStatus(number, request.DeliveryDate, request.StatusId);
            return NoContent();
        }
    }
}
