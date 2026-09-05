namespace BurgerX.Application.Shared.Interfaces;

public interface IUnitOfWork
{
    Task SaveAsync();
}