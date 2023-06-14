using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Services;

internal sealed class PostgresUserReadService : IUserReadService
{
    private readonly DbSet<User> _users;

    public PostgresUserReadService(TaskoPhobiaDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public async Task<bool> ExistsByEmailAsync(Email email)
    {
        return await _users.AnyAsync(x => x.Email == email);
    }

    public async Task<bool> ExistsByUsernameAsync(Username username)
    {
        return await _users.AnyAsync(x => x.Username == username);
    }

    public async Task<User> GetByEmailAsync(Email email)
    {
        return await _users.SingleOrDefaultAsync(x => x.Email == email);
    }
}