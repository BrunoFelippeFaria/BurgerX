using BurgerX.Domain.Exceptions;

namespace BurgerX.Application.Catalog.Products.Exceptions;

public class ProductNotFoundException(Guid id) : NotFoundException($"Produto {id} não encontrado.")
{
    public override string Code => "product_not_found";
}