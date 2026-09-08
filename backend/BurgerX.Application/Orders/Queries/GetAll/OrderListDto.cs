using BurgerX.Domain.Orders.Enums;

namespace BurgerX.Application.Orders.Queries.GetAll;

public record OrderListDto(
    Guid Id,
    int Number,
    string Customer,
    OrderType Type,
    decimal Total,
    OrderStatus Status
);