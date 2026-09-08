using BurgerX.Domain.Orders.Enums;
using BurgerX.Domain.Shared;
using BurgerX.Domain.Shared.ValueObjects;

namespace BurgerX.Domain.Entities.Orders;

public class Order : EntityBase
{
    public int Number { get; set; }
    public required string Customer { get; set; }
    public decimal Discount { get; set; }
    public OrderType Type { get; set; }
    public OrderStatus Status { get; set; }

    public Address? DeliveryAddress { get; set; }

    private readonly List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem> Items => _items;
    public decimal Total { get; set; }

    public void AddItem(OrderItem item)
    {
        _items.Add(item);
    }

    public void CalculateTotal()
    {
        Total = Items.Sum(i => i.Quantity * i.Price);

        if (Discount > 0)
            Total -= Total * (Discount / 100);
    }
}