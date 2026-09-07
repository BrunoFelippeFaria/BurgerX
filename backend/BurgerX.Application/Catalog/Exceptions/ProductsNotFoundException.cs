using BurgerX.Domain.Exceptions;

namespace BurgerX.Application.Catalog.Exceptions;

public class ProductsNotFoundException(IEnumerable<Guid> ids)
    : NotFoundException($"Os produtos {string.Join(", ", ids)} não foram encontrados.")
{
    public override string Code => "products_not_found";
}