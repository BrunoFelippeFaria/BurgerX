namespace BurgerX.Application.Catalog.Dtos;

public class ProductListDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
}