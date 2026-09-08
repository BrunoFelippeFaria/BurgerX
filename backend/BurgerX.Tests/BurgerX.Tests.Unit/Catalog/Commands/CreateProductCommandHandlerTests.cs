using BurgerX.Application.Catalog.Products;
using BurgerX.Application.Catalog.Products.Commands.Create;
using BurgerX.Application.Catalog.Products.Exceptions;
using BurgerX.Domain.Catalog.Products;

using FluentAssertions;

using NSubstitute;

namespace BurgerX.Tests.Unit.Catalog.Commands;

public class CreateProductCommandHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IProductDao _productDao;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _productDao = Substitute.For<IProductDao>();

        _handler = new(_productRepository, _productDao);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldReturnGeneratedId()
    {
        _productDao.NameExists(Arg.Any<string>()).Returns(false);

        var cmd = new CreateProductCommand {
            Name = "name",
            Description = "desc",
            Price = 10
        };

        var result = await _handler.Handle(cmd, default);

        result.Should().NotBeEmpty();

        _productRepository.Received(1).Add(Arg.Is<Product>(p =>
            p.Id == result &&
            p.Name == cmd.Name &&
            p.Description == cmd.Description &&
            p.Price == cmd.Price
        ));
    }

    [Fact]
    public async Task Handle_NameExists_ShouldThrowException()
    {
        _productDao.NameExists(Arg.Any<string>()).Returns(true);

        var cmd = new CreateProductCommand {
            Name = "name",
            Description = "desc",
            Price = 10
        };

        var act = async () => await _handler.Handle(cmd, default);

        await act.Should().ThrowAsync<ProductNameAlreadyExistsException>();
        _productRepository.DidNotReceive().Add(Arg.Any<Product>());
    }
}