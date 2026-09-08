using Mediator;

namespace BurgerX.Application.Orders.Queries.GetAll;

public record GetAllOrdersQuery : IRequest<IEnumerable<OrderListDto>>;