using System.ComponentModel.DataAnnotations;

namespace BurgerX.Infrastructure.Auth;

public class JwtOptions
{
    [Required]
    public required string SecretKey { get; init; }
    public int ExpirationDays { get; init; } = 30;
}