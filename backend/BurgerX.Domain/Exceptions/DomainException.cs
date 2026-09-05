namespace BurgerX.Domain.Exceptions;

public abstract class DomainException(string message) : Exception(message)
{
    public virtual string Code => "ERROR"; 
}