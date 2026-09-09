using BurgerX.Application.Catalog.Products.Exceptions;
using BurgerX.Domain.Catalog.Products;
using BurgerX.Domain.Entities;

using Mediator;

namespace BurgerX.Application.Catalog.Products.Commands.Create;

public class CreateProductCommandHandler(IProductRepository productRepository, IProductDao productDao)
    : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductDao _productDao = productDao;

    public async ValueTask<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (await _productDao.NameExists(request.Name))
            throw new ProductNameAlreadyExistsException(request.Name);

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        _productRepository.Add(product);
        return product.Id;
    }
}