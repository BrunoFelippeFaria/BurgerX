using BurgerX.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace BurgerX.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}