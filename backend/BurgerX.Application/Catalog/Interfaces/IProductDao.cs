using BurgerX.Application.Catalog.Dtos;
using BurgerX.Domain.Entities;

namespace BurgerX.Application.Catalog.Interfaces;

public interface IProductDao
{
    Task<IEnumerable<ProductListDto>> GetAll();
    Task<ProductDto?> GetById(Guid id);
}