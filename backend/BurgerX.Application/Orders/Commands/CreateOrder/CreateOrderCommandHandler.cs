using BurgerX.Application.Orders.Interfaces;
using BurgerX.Domain.Entities.Orders;

using Mediator;

namespace BurgerX.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IOrderRepository orderRepository, IOrderDao orderDao) 
    : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IOrderDao _orderDao = orderDao;

    public async ValueTask<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Guid orderId = Guid.NewGuid();

        var order = new Order
        {
            Id = orderId,
            Number = await _orderDao.GetNewNumber(),
            Customer = request.Customer,
            Discount = request.Discount,
            Status = request.Status,
            CreatedAt = DateTime.UtcNow,
        };

        foreach (var item in request.Items)
        {
            order.AddItem(new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                ProductId = item.ProductId,
                Note = item.Note,
                Quantity = item.Quantity,
                Price = item.Price,
                CreatedAt = DateTime.UtcNow,
            });
        }

        order.CalculateTotal();
        _orderRepository.Add(order);

        return orderId;
    }
}