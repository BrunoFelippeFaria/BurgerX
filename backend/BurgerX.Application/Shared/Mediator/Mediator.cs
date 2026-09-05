using BurgerX.Application.Shared.Mediator.Behaviours;

using Mediator;

using Microsoft.Extensions.DependencyInjection;

namespace BurgerX.Application.Shared.Mediator;

public static class Mediator
{
    public static IServiceCollection AddAppMediator(this IServiceCollection services)
    {
        services.AddMediator(options =>
        {
            options.Namespace = "BurgerX.Mediator";
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TranslacionalBehaviour<,>));


        return services;
    }
}