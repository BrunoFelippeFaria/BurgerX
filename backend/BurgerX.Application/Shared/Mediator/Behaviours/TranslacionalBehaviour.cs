

using BurgerX.Application.Shared.Interfaces;

using Mediator;

namespace BurgerX.Application.Shared.Mediator.Behaviours;

public class TranslacionalBehaviour<TMessage, TResponse>(IUnitOfWork uow) : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    private readonly IUnitOfWork _uow = uow;

    public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        if (message is not ITranslacionalRequest)
            return await next(message, cancellationToken);

        var response = await next(message, cancellationToken);
        await _uow.SaveAsync();

        return response;
    }

}