namespace BurgerX.Domain.Shared;

public class EntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }

    public void Delete() => IsDeleted = true;
}