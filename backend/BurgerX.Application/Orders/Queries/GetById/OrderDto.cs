using BurgerX.Application.Orders.Dtos;
using BurgerX.Domain.Enums;
using BurgerX.Domain.ValueObjects;

namespace BurgerX.Application.Orders.Queries.GetById;

public record OrderDto(
    Guid Id,
    int Number,
    string Customer,
    decimal Discount,
    OrderType Type,
    decimal Total,
    OrderStatus Status,
    Address? DeliveryAddress,
    IEnumerable<OrderItemDto> Items
);