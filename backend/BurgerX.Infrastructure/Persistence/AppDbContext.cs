using Microsoft.EntityFrameworkCore;

namespace BurgerX.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    
}