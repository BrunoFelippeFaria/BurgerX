using BurgerX.Application.Catalog.Commands.UpdateProduct;
using BurgerX.Application.Catalog.Exceptions;
using BurgerX.Application.Catalog.Interfaces;
using BurgerX.Domain.Entities;

using FluentAssertions;

using NSubstitute;

namespace BurgerX.Tests.Unit.Catalog.Commands;

public class UpdateProductCommandHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IProductDao _productDao;
    private readonly UpdateProductCommandHandler _handler;

    public UpdateProductCommandHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _productDao = Substitute.For<IProductDao>();

        _handler = new(_productRepository, _productDao);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldUpdateProductFields()
    {
        var productId = Guid.NewGuid();

        var existingProduct = new Product
        {
            Id = productId,
            Name = "old name",
            Description = "old desc",
            Price = 5
        };

        _productDao.NameExists(Arg.Any<string>(), Arg.Any<Guid>()).Returns(false);
        _productRepository.GetById(productId).Returns(existingProduct);

        var cmd = new UpdateProductCommand
        {
            Id = productId,
            Name = "new name",
            Description = "new desc",
            Price = 20
        };

        await _handler.Handle(cmd, default);

        existingProduct.Name.Should().Be(cmd.Name);
        existingProduct.Description.Should().Be(cmd.Description);
        existingProduct.Price.Should().Be(cmd.Price);
    }

    [Fact]
    public async Task Handle_NameExists_ShouldThrowException_AndNotFetchProduct()
    {
        _productDao.NameExists(Arg.Any<string>(), Arg.Any<Guid>()).Returns(true);

        var cmd = new UpdateProductCommand
        {
            Id = Guid.NewGuid(),
            Name = "name",
            Description = "desc",
            Price = 10
        };

        var act = async () => await _handler.Handle(cmd, default);

        await act.Should().ThrowAsync<ProductNameAlreadyExistsException>();
        await _productRepository.DidNotReceive().GetById(Arg.Any<Guid>());
    }

    [Fact]
    public async Task Handle_ProductNotFound_ShouldThrowException()
    {
        var productId = Guid.NewGuid();

        _productDao.NameExists(Arg.Any<string>(), Arg.Any<Guid>()).Returns(false);
        _productRepository.GetById(productId).Returns((Product?)null);

        var cmd = new UpdateProductCommand
        {
            Id = productId,
            Name = "name",
            Description = "desc",
            Price = 10
        };

        var act = async () => await _handler.Handle(cmd, default);

        await act.Should().ThrowAsync<ProductNotFoundException>();
    }
}