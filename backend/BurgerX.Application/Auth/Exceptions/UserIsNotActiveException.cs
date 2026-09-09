using BurgerX.Domain.Shared.Exceptions;

namespace BurgerX.Application.Auth.Exceptions;

public class UserIsNotActiveException() 
    : UnauthorizedException($"usuário não está ativo.")
{
    
}