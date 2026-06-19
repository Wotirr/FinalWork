namespace KnizhnyMir.Api.Dtos
{
    /// <summary>Запрос на вход в систему.</summary>
    public record LoginRequest(string Login, string Password);

    /// <summary>Позиция заказа в запросе на создание заказа.</summary>
    public record OrderItemRequest(string ProductArticle, int Quantity);

    /// <summary>Запрос на создание нового заказа.</summary>
    public record CreateOrderRequest(int? UserId, List<OrderItemRequest> Items);

    /// <summary>Запрос на обновление даты доставки и статуса заказа.</summary>
    public record UpdateOrderRequest(DateTime? DeliveryDate, int StatusId);
}
