using Mediator;

namespace BurgerX.Application.Auth.Commands.Login;

public record LoginCommand (string UserName, string Password) : IRequest<string>;