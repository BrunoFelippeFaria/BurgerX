using BurgerX.Application.Shared.Interfaces;
using BurgerX.Domain.Orders.Enums;
using BurgerX.Domain.Shared.ValueObjects;

using Mediator;

namespace BurgerX.Application.Orders.Commands.Create;

public record CreateOrderCommand(
    string Customer,
    decimal Discount,
    OrderType Type,
    Address? DeliveryAddress,
    IEnumerable<OrderItemAddDto> Items
) : IRequest<Guid>, ITranslacionalRequest;