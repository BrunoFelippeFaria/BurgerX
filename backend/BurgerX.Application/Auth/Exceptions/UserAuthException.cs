using BurgerX.Domain.Shared.Exceptions;

namespace BurgerX.Application.Auth.Exceptions;

public class UserAuthException() 
    : UnauthorizedException($"usuário ou senha invalida.")
{
    
}