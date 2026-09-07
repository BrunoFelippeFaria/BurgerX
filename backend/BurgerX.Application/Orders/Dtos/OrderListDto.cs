using BurgerX.Domain.Enums;

namespace BurgerX.Application.Orders.Dtos;

public record OrderListDto(
    Guid Id,
    int Number,
    string Customer,
    OrderType Type,
    decimal Total,
    OrderStatus Status
);