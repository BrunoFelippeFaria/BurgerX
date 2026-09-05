using BurgerX.Application.Catalog.Commands.DeleteProduct;
using BurgerX.Application.Catalog.Exceptions;
using BurgerX.Application.Catalog.Interfaces;
using BurgerX.Domain.Entities;

using FluentAssertions;

using NSubstitute;

namespace BurgerX.Tests.Unit.Catalog.Commands;

public class DeleteProductCommandHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly DeleteProductCommandHandler _handler;

    public DeleteProductCommandHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new(_productRepository);
    }

    [Fact]
    public async Task Handle_ExistingProduct_ShouldCallDelete()
    {
        var productId = Guid.NewGuid();

        var existingProduct = new Product
        {
            Id = productId,
            Name = "name",
            Description = "desc",
            Price = 10
        };

        _productRepository.GetById(productId).Returns(existingProduct);

        var cmd = new DeleteProductCommand(productId);

        await _handler.Handle(cmd, default);
        existingProduct.IsDeleted.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ProductNotFound_ShouldThrowException()
    {
        var productId = Guid.NewGuid();

        _productRepository.GetById(productId).Returns((Product?)null);

        var cmd = new DeleteProductCommand(productId);

        var act = async () => await _handler.Handle(cmd, default);

        await act.Should().ThrowAsync<ProductNotFoundException>();
    }
}