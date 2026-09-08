using Mediator;

namespace BurgerX.Application.Orders.Queries.GetById;

public record GetOrderByIdQuery (Guid Id) : IRequest<OrderDto>;