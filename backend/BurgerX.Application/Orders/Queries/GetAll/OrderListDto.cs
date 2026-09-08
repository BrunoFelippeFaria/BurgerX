using BurgerX.Domain.Enums;

namespace BurgerX.Application.Orders.Queries.GetAll;

public record OrderListDto(
    Guid Id,
    int Number,
    string Customer,
    OrderType Type,
    decimal Total,
    OrderStatus Status
);