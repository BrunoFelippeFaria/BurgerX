namespace BurgerX.Domain.Entities;

public class EntityBase
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
}