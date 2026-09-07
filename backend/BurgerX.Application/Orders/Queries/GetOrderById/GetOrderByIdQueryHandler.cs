
using BurgerX.Application.Orders.Dtos;
using BurgerX.Application.Orders.Exceptions;
using BurgerX.Application.Orders.Interfaces;

using Mediator;

namespace BurgerX.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler (IOrderDao orderDao) : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderDao _orderDao = orderDao;

    public async ValueTask<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _orderDao.GetById(request.Id)
            ?? throw new OrderNotFoundException(request.Id);
    }
}