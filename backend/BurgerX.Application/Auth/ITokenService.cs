using BurgerX.Domain.Administration;

namespace BurgerX.Application.Auth;

public interface ITokenService
{
    public string Generate(User user);
}