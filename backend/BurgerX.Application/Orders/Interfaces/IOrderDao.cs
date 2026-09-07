using BurgerX.Application.Orders.Dtos;

namespace BurgerX.Application.Orders.Interfaces;

public interface IOrderDao
{
    Task<IEnumerable<OrderListDto>> GetAll();
    Task<int> GetNewNumber(); // sqlite não tem autoincrement fora de pk :(
    Task<OrderDto?> GetById(Guid id);
}