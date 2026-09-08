using BurgerX.Application.Catalog.Products;
using BurgerX.Application.Catalog.Products.Exceptions;
using BurgerX.Domain.Entities.Orders;
using BurgerX.Domain.Orders.Enums;
using BurgerX.Domain.Shared.ValueObjects;

using Mediator;

namespace BurgerX.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IOrderDao orderDao,
    IProductRepository productRepository
) 
    : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IOrderDao _orderDao = orderDao;
    private readonly IProductRepository _productRepository = productRepository;

    public async ValueTask<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Guid orderId = Guid.NewGuid();

        var productIds = request.Items
            .Select(x => x.ProductId)
            .Distinct();

        var products = await _productRepository.GetByIds(productIds);
        var productsById = products.ToDictionary(p => p.Id);

        var missingIds = productIds
            .Except(products.Select(x => x.Id))
            .ToList();

        if (missingIds.Count > 0)
            throw new ProductsNotFoundException(missingIds);

        var order = new Order
        {
            Id = orderId,
            Number = await _orderDao.GetNewNumber(),
            Customer = request.Customer,
            Discount = request.Discount,
            Type = request.Type,
            Status = OrderStatus.Ordered,
            CreatedAt = DateTime.UtcNow,
        };

        if (order.Type == OrderType.Delivery)
        {
            var address = request.DeliveryAddress!;

            order.DeliveryAddress = new Address {
                ZipCode = address.ZipCode,
                Street = address.Street,
                Number = address.Number,
                Complement = address.Complement,
                Neighborhood = address.Neighborhood,
                City = address.City,
                State = address.State,
            };
        }


        foreach (var item in request.Items)
        {
            order.AddItem(new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                ProductId = item.ProductId,
                Note = item.Note,
                Quantity = item.Quantity,
                Price = productsById[item.ProductId].Price,
                CreatedAt = DateTime.UtcNow,
            });
        }

        order.CalculateTotal();
        _orderRepository.Add(order);

        return orderId;
    }
}