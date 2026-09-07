using Mediator;

namespace BurgerX.Application.Catalog.Products.Queries.GetAll;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductListDto>>;