using BurgerX.Application.Orders.Exceptions;

using Mediator;

namespace BurgerX.Application.Orders.Queries.GetById;

public class GetOrderByIdQueryHandler (IOrderDao orderDao) : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderDao _orderDao = orderDao;

    public async ValueTask<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _orderDao.GetById(request.Id)
            ?? throw new OrderNotFoundException(request.Id);
    }
}