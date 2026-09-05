using BurgerX.Application.Catalog.Dtos;

using Mediator;

namespace BurgerX.Application.Catalog.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;