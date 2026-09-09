using BurgerX.Application.Catalog.Products;
using BurgerX.Application.Catalog.Products.Exceptions;
using BurgerX.Application.Catalog.Products.Queries.GetById;

using FluentAssertions;

using NSubstitute;

namespace BurgerX.Tests.Unit.Catalog.Products.Queries;

public class GetProductByIdQueryHandlerTests
{
    private readonly IProductDao _productDao;
    private readonly GetProductByIdQueryHandler _handler;

    public GetProductByIdQueryHandlerTests()
    {
        _productDao = Substitute.For<IProductDao>();
        _handler = new(_productDao);
    }

    [Fact]
    public async Task Handle_ExistingProduct_ShouldReturnDto()
    {
        var productId = Guid.NewGuid();

        var expectedDto = new ProductDto
        {
            Id = productId,
            Name = "name",
            Description = "desc",
            Price = 10
        };

        _productDao.GetById(productId).Returns(expectedDto);

        var query = new GetProductByIdQuery(productId);

        var result = await _handler.Handle(query, default);

        result.Should().BeEquivalentTo(expectedDto);
    }

    [Fact]
    public async Task Handle_ProductNotFound_ShouldThrowException()
    {
        var productId = Guid.NewGuid();

        _productDao.GetById(productId).Returns((ProductDto?)null);

        var query = new GetProductByIdQuery(productId);

        var act = async () => await _handler.Handle(query, default);

        await act.Should().ThrowAsync<ProductNotFoundException>();
    }
}