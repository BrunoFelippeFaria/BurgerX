using BurgerX.Application.Catalog.Exceptions;
using BurgerX.Application.Catalog.Interfaces;
using Mediator;

namespace BurgerX.Application.Catalog.Commands.UpdateProduct;

public class UpdateProductCommandHandler (IProductRepository productRepository) : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async ValueTask<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.Id)
            ?? throw new ProductNotFoundException(request.Id);

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;

        return Unit.Value;
    }
}