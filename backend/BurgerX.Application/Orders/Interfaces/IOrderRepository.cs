using BurgerX.Domain.Entities.Orders;

namespace BurgerX.Application.Orders.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetById(Guid id);
    void Add(Order order);
}