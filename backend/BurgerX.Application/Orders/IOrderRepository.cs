using BurgerX.Domain.Entities.Orders;

namespace BurgerX.Application.Orders;

public interface IOrderRepository
{
    Task<Order?> GetById(Guid id);
    void Add(Order order);
}