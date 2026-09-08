using BurgerX.Application.Catalog.Products;
using BurgerX.Application.Catalog.Products.Commands.Create;
using BurgerX.Application.Orders;
using BurgerX.Application.Shared.Interfaces;
using BurgerX.Application.Shared.Mediator;
using BurgerX.Infrastructure.Persistence;
using BurgerX.Infrastructure.Persistence.Daos;
using BurgerX.Infrastructure.Persistence.Repositories;

using FluentValidation;

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

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddAppMediator();

        services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly);
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductDao, ProductDao>();
        services.AddScoped<IOrderDao, OrderDao>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}