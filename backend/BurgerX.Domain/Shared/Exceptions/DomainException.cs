namespace BurgerX.Domain.Shared.Exceptions;

public abstract class DomainException(string message) : Exception(message)
{
    public virtual string Code => "ERROR";
}