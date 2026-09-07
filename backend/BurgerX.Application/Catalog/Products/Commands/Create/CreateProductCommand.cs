using BurgerX.Application.Shared.Interfaces;

using Mediator;

namespace BurgerX.Application.Catalog.Products.Commands.Create;

public record CreateProductCommand : IRequest<Guid>, ITranslacionalRequest
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
}