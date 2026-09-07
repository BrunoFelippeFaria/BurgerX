using BurgerX.Application.Orders.Dtos;

using Mediator;

namespace BurgerX.Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery (Guid Id) : IRequest<OrderDto>;