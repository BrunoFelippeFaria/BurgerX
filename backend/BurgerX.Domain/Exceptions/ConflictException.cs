namespace BurgerX.Domain.Exceptions;

public class ConflictException(string message) : DomainException(message)
{
    public override string Code => "conflict";
}