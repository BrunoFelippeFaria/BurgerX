using BurgerX.Domain.Entities;
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
}