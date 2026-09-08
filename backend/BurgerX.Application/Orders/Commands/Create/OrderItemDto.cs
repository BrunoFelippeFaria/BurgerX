namespace BurgerX.Application.Orders.Dtos;

public record OrderItemDto(
    string Product,    
    int Quantity,
    decimal Price,
    string? Note
);