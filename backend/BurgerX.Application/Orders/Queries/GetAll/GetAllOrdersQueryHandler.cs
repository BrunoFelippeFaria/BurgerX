using Mediator;

namespace BurgerX.Application.Orders.Queries.GetAll;

public class GetAllOrdersQueryHandler(IOrderDao orderDao) : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderListDto>>
{
    private readonly IOrderDao _orderDao = orderDao;

    public async ValueTask<IEnumerable<OrderListDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _orderDao.GetAll();
    }
}