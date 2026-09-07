using BurgerX.Application.Orders.Dtos;

using Mediator;

namespace BurgerX.Application.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery : IRequest<IEnumerable<OrderListDto>>;