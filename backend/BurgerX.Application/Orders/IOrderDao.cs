using BurgerX.Application.Orders.Queries.GetAll;
using BurgerX.Application.Orders.Queries.GetById;

namespace BurgerX.Application.Orders;

public interface IOrderDao
{
    Task<IEnumerable<OrderListDto>> GetAll();
    Task<int> GetNewNumber(); // sqlite não tem autoincrement fora de pk :(
    Task<OrderDto?> GetById(Guid id);
}