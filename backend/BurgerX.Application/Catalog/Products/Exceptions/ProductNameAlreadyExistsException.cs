using BurgerX.Domain.Shared.Exceptions;

namespace BurgerX.Application.Catalog.Products.Exceptions;

public class ProductNameAlreadyExistsException(string name)
    : ConflictException($"já existe um produto com o nome {name}.")
{
    public override string Code => "product_name_already_exists";
}