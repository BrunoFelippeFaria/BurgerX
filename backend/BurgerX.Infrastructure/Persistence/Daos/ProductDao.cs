using BurgerX.Application.Catalog.Products;
using BurgerX.Application.Catalog.Products.Queries.GetAll;
using BurgerX.Application.Catalog.Products.Queries.GetById;
using BurgerX.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;

namespace BurgerX.Infrastructure.Persistence.Daos;

public class ProductDao (AppDbContext context) : IProductDao
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<ProductListDto>> GetAll()
    {
        return await _context.Products
            .AsNoTracking()
            .Select(p => new ProductListDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            })
            .ToArrayAsync();
    }

    public async Task<ProductDto?> GetById(Guid id)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).FirstOrDefaultAsync();
    }

    public async Task<bool> NameExists(string name, Guid? ignoredId = null)
    {
        return await _context.Products
            .WhereIf(ignoredId.HasValue, p => p.Id != ignoredId)
            .AnyAsync(p => p.Name == name);
    }
}