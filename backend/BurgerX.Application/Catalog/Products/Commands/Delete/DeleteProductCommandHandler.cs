using BurgerX.Application.Catalog.Products.Exceptions;

using Mediator;

namespace BurgerX.Application.Catalog.Products.Commands.Delete;

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