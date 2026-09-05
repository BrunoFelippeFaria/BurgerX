namespace BurgerX.Domain.Entities.Orders;

public class OrderItem : EntityBase
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? Note { get; set; }
}