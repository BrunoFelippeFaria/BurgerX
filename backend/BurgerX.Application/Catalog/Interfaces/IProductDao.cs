using BurgerX.Application.Catalog.Dtos;
using BurgerX.Domain.Entities;

namespace BurgerX.Application.Catalog.Interfaces;

public interface IProductDao
{
    public Task<IEnumerable<ProductListDto>> GetAll();
}