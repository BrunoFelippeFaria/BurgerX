namespace BurgerX.Domain.Shared.Exceptions;

public class NotFoundException(string message) : DomainException(message)
{
    public override string Code => "not_found";
}