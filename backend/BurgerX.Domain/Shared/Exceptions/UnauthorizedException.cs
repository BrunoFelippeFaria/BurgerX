namespace BurgerX.Domain.Shared.Exceptions;

public class UnauthorizedException(string message) : DomainException(message)
{
    public override string Code => "unauthorized";
}