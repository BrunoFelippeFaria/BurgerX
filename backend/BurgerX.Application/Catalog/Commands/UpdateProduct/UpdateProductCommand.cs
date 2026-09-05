using BurgerX.Application.Shared.Interfaces;

using Mediator;

namespace BurgerX.Application.Catalog.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest<Unit>, ITranslacionalRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
}