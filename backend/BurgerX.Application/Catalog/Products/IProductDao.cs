using BurgerX.Application.Catalog.Products.Queries.GetAll;
using BurgerX.Application.Catalog.Products.Queries.GetById;

namespace BurgerX.Application.Catalog.Products;

public interface IProductDao
{
    Task<IEnumerable<ProductListDto>> GetAll();
    Task<ProductDto?> GetById(Guid id);
    Task<bool> NameExists(string name, Guid? ignoredId = null);
}