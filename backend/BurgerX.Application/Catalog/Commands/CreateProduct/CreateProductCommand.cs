using Mediator;

namespace BurgerX.Application.Catalog.Commands.CreateProduct;

public record CreateProductCommand : IRequest<Guid>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
}