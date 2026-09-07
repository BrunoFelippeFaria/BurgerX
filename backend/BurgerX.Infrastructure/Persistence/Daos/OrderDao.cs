
using BurgerX.Application.Orders.Dtos;
using BurgerX.Application.Orders.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace BurgerX.Infrastructure.Persistence.Daos;

public class OrderDao(AppDbContext context) : IOrderDao
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<OrderListDto>> GetAll()
    {
        return await _context.Orders
            .AsNoTracking()
            .Select(o => new OrderListDto(
                o.Number,
                o.Customer,
                o.Type,
                o.Total,
                o.Status
            )).ToArrayAsync();
    }

    public Task<OrderDto> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetNewNumber()
    {
        return await _context.Orders
            .MaxAsync(o => o.Number) + 1;
    }
}