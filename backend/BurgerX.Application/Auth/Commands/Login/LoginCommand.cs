using Mediator;

namespace BurgerX.Application.Auth.Commands.Login;

public record LoginCommand : IRequest<string>
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}