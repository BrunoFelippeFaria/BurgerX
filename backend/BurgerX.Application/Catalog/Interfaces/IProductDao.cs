using BurgerX.Application.Catalog.Dtos;

namespace BurgerX.Application.Catalog.Interfaces;

public interface IProductDao
{
    Task<IEnumerable<ProductListDto>> GetAll();
    Task<ProductDto?> GetById(Guid id);
    Task<bool> NameExists(string name, Guid? ignoredId = null);
}