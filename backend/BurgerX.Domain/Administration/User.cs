using BurgerX.Domain.Shared;

namespace BurgerX.Domain.Administration;

public class User : EntityBase
{
    public required string Name { get; set; }
    public required string Username { get; set; }
    public bool IsActive { get; set; } = true;
    public required string Hash { get; set; }
}