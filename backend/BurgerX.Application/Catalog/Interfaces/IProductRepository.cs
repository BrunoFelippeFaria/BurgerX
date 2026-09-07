using BurgerX.Domain.Entities;

namespace BurgerX.Application.Catalog.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetById(Guid id);
    void Add(Product product);
    Task<IReadOnlyCollection<Product>> GetByIds(IEnumerable<Guid> ids);
}