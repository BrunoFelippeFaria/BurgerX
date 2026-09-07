using BurgerX.Application.Catalog.Products;
using BurgerX.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace BurgerX.Infrastructure.Persistence.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    private readonly AppDbContext _context = context;

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyCollection<Product>> GetByIds(IEnumerable<Guid> ids)
    {
        return await _context.Products
            .Where(p => ids.Contains(p.Id))
            .ToArrayAsync();
    }
}