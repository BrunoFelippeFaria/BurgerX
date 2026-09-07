using Mediator;

namespace BurgerX.Application.Catalog.Products.Queries.GetById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;