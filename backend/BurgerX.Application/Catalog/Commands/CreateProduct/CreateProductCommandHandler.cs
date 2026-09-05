
using BurgerX.Application.Catalog.Interfaces;
using BurgerX.Domain.Entities;

using Mediator;

namespace BurgerX.Application.Catalog.Commands.CreateProduct;

public class CreateProductCommandHandler (IProductRepository productRepository) 
    : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository = productRepository;

    public ValueTask<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        _productRepository.Add(product);
        return ValueTask.FromResult(product.Id);
    }
}