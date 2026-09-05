using BurgerX.Application.Shared.Interfaces;

using Mediator;

namespace BurgerX.Application.Catalog.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<Unit>, ITranslacionalRequest;
