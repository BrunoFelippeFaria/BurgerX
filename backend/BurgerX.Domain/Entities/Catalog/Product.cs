namespace BurgerX.Domain.Entities;

public class Product : EntityBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
}