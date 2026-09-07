using BurgerX.Application.Orders.Dtos;
using BurgerX.Application.Shared.Interfaces;
using BurgerX.Domain.Enums;
using BurgerX.Domain.ValueObjects;

using Mediator;

namespace BurgerX.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    string Customer,
    decimal Discount,
    OrderType Type,
    Address? DeliveryAddress,
    IEnumerable<OrderItemAddDto> Items
) : IRequest<Guid>, ITranslacionalRequest;