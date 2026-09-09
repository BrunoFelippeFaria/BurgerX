using BurgerX.Domain.Administration;

namespace BurgerX.Application.Auth;

public interface IUserRepository
{
    Task<User?> GetById(Guid id);
    public void Add(User user);
}