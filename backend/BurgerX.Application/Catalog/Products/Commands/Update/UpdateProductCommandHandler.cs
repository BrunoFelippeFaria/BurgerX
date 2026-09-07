using BurgerX.Application.Catalog.Products.Exceptions;

using Mediator;

namespace BurgerX.Application.Catalog.Products.Commands.Update;

public class UpdateProductCommandHandler (IProductRepository productRepository, IProductDao productDao) 
    : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductDao _productDao = productDao;

    public async ValueTask<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        if (await _productDao.NameExists(request.Name, request.Id))
            throw new ProductNameAlreadyExistsException(request.Name);

        var product = await _productRepository.GetById(request.Id)
            ?? throw new ProductNotFoundException(request.Id);

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;

        return Unit.Value;
    }
}