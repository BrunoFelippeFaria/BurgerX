using BurgerX.Application.Auth;
using BurgerX.Domain.Administration;

using Microsoft.EntityFrameworkCore;

namespace BurgerX.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<User?> GetById(Guid id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUserName(string userName)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == userName);
    }


    public void Add(User user)
    {
        _context.Users.Add(user);
    }
}