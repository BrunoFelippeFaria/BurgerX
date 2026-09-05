using BurgerX.Application.Catalog.Interfaces;
using BurgerX.Infrastructure.Persistence;
using BurgerX.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerX.CrossCutting.DependencyInjection;

public static class Dependencies
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("Default"))
        );

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}