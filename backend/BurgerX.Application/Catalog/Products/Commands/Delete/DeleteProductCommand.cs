using BurgerX.Application.Shared.Interfaces;

using Mediator;

namespace BurgerX.Application.Catalog.Products.Commands.Delete;

public record DeleteProductCommand(Guid Id) : IRequest<Unit>, ITranslacionalRequest;
