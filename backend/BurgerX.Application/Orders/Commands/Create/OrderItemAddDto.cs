using Mediator;

namespace BurgerX.Application.Orders.Commands.Create;

public record OrderItemAddDto(
    Guid ProductId,
    int Quantity,
    string? Note
);