using BurgerX.Application.Catalog.Dtos;

using Mediator;

namespace BurgerX.Application.Catalog.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductListDto>>;