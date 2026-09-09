using BurgerX.Application.Catalog.Products;
using BurgerX.Application.Catalog.Products.Exceptions;
using BurgerX.Application.Orders;
using BurgerX.Application.Orders.Commands.CreateOrder;
using BurgerX.Domain.Catalog.Products;
using BurgerX.Domain.Entities.Orders;
using BurgerX.Domain.Orders.Enums;

using FluentAssertions;

using NSubstitute;

namespace BurgerX.Tests.Unit.Orders.Commands;

public class CreateOrderCommandHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDao _orderDao;
    private readonly IProductRepository _productRepository;

    private readonly CreateOrderCommandHandler _handler;

    public CreateOrderCommandHandlerTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _orderDao = Substitute.For<IOrderDao>();
        _productRepository = Substitute.For<IProductRepository>();

        _handler = new(_orderRepository, _orderDao, _productRepository);
    }

    [Fact]
    public async Task Handle_ProductNotFound_ShouldThrowException()
    {
        var missingProductId = Guid.NewGuid();

        _productRepository.GetByIds(Arg.Any<IEnumerable<Guid>>())
            .Returns([]);

        var cmd = new CreateOrderCommand(
            "customer",
            0,
            OrderType.DineIn,
            null,
            [
                new(missingProductId, 1, null)
            ]
        );

        var act = async () => await _handler.Handle(cmd, default);

        await act.Should().ThrowAsync<ProductsNotFoundException>();
    }

    [Fact]
    public async Task Handle_Sucess_ShouldCalculateTotal()
    {
        var product1 = new Product { Id = Guid.NewGuid(), Price = 10m, Description = "desc", Name = "product1" };
        var product2 = new Product { Id = Guid.NewGuid(), Price = 5m, Description = "desc", Name = "product2" };

        _productRepository.GetByIds(Arg.Any<IEnumerable<Guid>>())
            .Returns([product1, product2]);

        _orderDao.GetNewNumber().Returns(1);

        Order? capturedOrder = null;
        _orderRepository
            .When(x => x.Add(Arg.Any<Order>()))
            .Do(x => capturedOrder = x.Arg<Order>());

        var discount = 3m;

        var cmd = new CreateOrderCommand(
            "customer",
            discount,
            OrderType.DineIn,
            null,
            [
                new(product1.Id, 2, null), // 2 * 10 = 20
                new(product2.Id, 1, null), // 1 * 5  = 5
            ]
        );

        await _handler.Handle(cmd, default);

        var subtotal = (2 * product1.Price) + (1 * product2.Price); // 25
        var expectedTotal = subtotal - (subtotal * (discount / 100)); // 25 - 2.5 = 22.5

        capturedOrder.Should().NotBeNull();
        capturedOrder!.Total.Should().Be(expectedTotal);
    }

    [Fact]
    public async Task Handle_Sucess_ShouldReturnId()
    {
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId, Price = 10m, Description = "desc", Name = "name" };

        _productRepository.GetByIds(Arg.Any<IEnumerable<Guid>>())
            .Returns([product]);

        _orderDao.GetNewNumber().Returns(1);

        Order? capturedOrder = null;
        _orderRepository
            .When(x => x.Add(Arg.Any<Order>()))
            .Do(x => capturedOrder = x.Arg<Order>());

        var cmd = new CreateOrderCommand(
            "customer",
            0,
            OrderType.DineIn,
            null,
            [
                new(productId, 2, null)
            ]
        );

        var result = await _handler.Handle(cmd, default);

        result.Should().NotBe(Guid.Empty);
        capturedOrder.Should().NotBeNull();
        capturedOrder!.Id.Should().Be(result);
    }

}