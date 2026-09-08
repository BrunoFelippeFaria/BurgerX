
using BurgerX.Application.Orders;
using BurgerX.Application.Orders.Dtos;
using BurgerX.Application.Orders.Queries.GetAll;
using BurgerX.Application.Orders.Queries.GetById;

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
                o.Id,
                o.Number,
                o.Customer,
                o.Type,
                o.Total,
                o.Status
            )).ToArrayAsync();
    }

    public async Task<OrderDto?> GetById(Guid id)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(o => o.Id == id)
            .Select(o => new OrderDto(
                o.Id,
                o.Number,
                o.Customer,
                o.Discount,
                o.Type,
                o.Total,
                o.Status,
                o.DeliveryAddress,
                o.Items.Select(i => new OrderItemDto(
                    i.Product.Name,
                    i.Quantity,
                    i.Price,
                    i.Note
                ))
            ))
            .FirstOrDefaultAsync();
    }

    public async Task<int> GetNewNumber()
    {
        var maxNumber = await _context.Orders
            .MaxAsync(o => (int?)o.Number) ?? 0;

        return maxNumber + 1;
    }
}