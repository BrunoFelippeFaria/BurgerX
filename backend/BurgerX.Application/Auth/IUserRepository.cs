using BurgerX.Domain.Administration;

namespace BurgerX.Application.Auth;

public interface IUserRepository
{
    Task<User?> GetById(Guid id);
    Task<User?> GetByUserName(string userName);
    public void Add(User user);
}