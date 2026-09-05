using BurgerX.Domain.Entities;

namespace BurgerX.Application.Catalog.Interfaces;

public interface IProductRepository
{
    public Task<Product?> GetById(Guid id);
    public void Add(Product product);
}