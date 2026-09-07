using Mediator;

namespace BurgerX.Application.Orders.Dtos;

public record OrderItemDto(
    Guid ProductId,
    int Quantity,
    decimal Price,
    string? Note
);