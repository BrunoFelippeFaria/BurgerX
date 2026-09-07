using Mediator;

namespace BurgerX.Application.Orders.Dtos;

public record OrderItemAddDto(
    Guid ProductId,
    int Quantity,
    string? Note
);