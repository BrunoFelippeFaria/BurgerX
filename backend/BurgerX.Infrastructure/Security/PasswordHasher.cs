using BurgerX.Application.Auth;
using Microsoft.AspNetCore.Identity;

namespace BurgerX.Infrastructure.Security;

public class PasswordHasher() : IHasher
{
    private readonly PasswordHasher<object> _passwordHasher = new();

    public string Hash(string value)
    {
        return _passwordHasher.HashPassword(null!, value);
    }

    public bool Verify(string value, string hash)
    {
        return _passwordHasher.VerifyHashedPassword(null!, hash, value) 
            != PasswordVerificationResult.Failed;
    }
}