using BurgerX.Domain.Enums;

namespace BurgerX.Application.Orders.Dtos;

public record OrderListDto(
    int Number,
    string Customer,
    OrderType Type,
    decimal Total,
    OrderStatus Status
);