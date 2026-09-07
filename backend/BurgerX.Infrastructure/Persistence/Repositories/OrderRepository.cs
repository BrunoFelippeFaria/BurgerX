
using BurgerX.Application.Orders.Interfaces;
using BurgerX.Domain.Entities.Orders;

using Microsoft.EntityFrameworkCore;

namespace BurgerX.Infrastructure.Persistence.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    private readonly AppDbContext _context = context;

    public void Add(Order order)
    {
        _context.Orders.Add(order);
    }

    public async Task<Order?> GetById(Guid id)
    {
        return await _context.Orders
            .Where(o => o.Id == id)
            .Include(o => o.Items)
            .Include(o => o.DeliveryAddress)
            .FirstOrDefaultAsync();
    }
}