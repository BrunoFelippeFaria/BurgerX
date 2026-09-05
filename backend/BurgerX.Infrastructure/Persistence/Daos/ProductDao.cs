

using BurgerX.Application.Catalog.Dtos;
using BurgerX.Application.Catalog.Interfaces;

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
}