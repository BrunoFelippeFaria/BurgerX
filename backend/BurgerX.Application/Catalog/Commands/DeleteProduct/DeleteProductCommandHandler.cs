
using BurgerX.Application.Catalog.Exceptions;
using BurgerX.Application.Catalog.Interfaces;

using Mediator;

namespace BurgerX.Application.Catalog.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async ValueTask<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.Id)
            ?? throw new ProductNotFoundException(request.Id);
            
        product.Delete();

        return Unit.Value;
    }
}
