
using BurgerX.Application.Catalog.Dtos;
using BurgerX.Application.Catalog.Exceptions;
using BurgerX.Application.Catalog.Interfaces;

using Mediator;

namespace BurgerX.Application.Catalog.Queries.GetProductById;

public class GetProductByIdQueryHandler(IProductDao productDao) : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductDao _productDao = productDao;

    public async ValueTask<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productDao.GetById(request.Id)
            ?? throw new ProductNotFoundException(request.Id);
            
        return product;
    }
}