using BurgerX.Application.Catalog.Products;

using Mediator;

namespace BurgerX.Application.Catalog.Products.Queries.GetAll;

public class GetAllProductsQueryHandler (IProductDao productDao) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductListDto>>
{
    private readonly IProductDao _productDao = productDao;

    public async ValueTask<IEnumerable<ProductListDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productDao.GetAll();
        return products;
    }
}