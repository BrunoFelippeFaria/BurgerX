namespace BurgerX.Application.Orders.Commands.Create;

public record OrderItemDto(
    string Product,
    int Quantity,
    decimal Price,
    string? Note
);