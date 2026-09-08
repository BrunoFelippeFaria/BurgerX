using BurgerX.Domain.Catalog.Products;
using BurgerX.Domain.Entities.Orders;
using BurgerX.Infrastructure.Persistence.EntityConfiguration;

using Microsoft.EntityFrameworkCore;

namespace BurgerX.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

}