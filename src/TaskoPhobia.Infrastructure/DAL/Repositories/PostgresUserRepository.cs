using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Infrastructure.DAL.Contexts;

namespace TaskoPhobia.Infrastructure.DAL.Repositories;

internal sealed class PostgresUserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public PostgresUserRepository(TaskoPhobiaWriteDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public Task<User> GetByIdAsync(UserId id)
    {
        return _users
            .Include(x => x.OwnedProjects)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(User user)
    {
        await _users.AddAsync(user);
    }

    public Task UpdateAsync(User user)
    {
        _users.Update(user);
        return Task.CompletedTask;
    }

    public Task<User> GetByEmailAsync(Email email)
    {
        return _users
            .Include(x => x.OwnedProjects)
            .SingleOrDefaultAsync(x => x.Email == email);
    }

    public Task<User> GetByUsernameAsync(Username username)
    {
        return _users
            .Include(x => x.OwnedProjects)
            .SingleOrDefaultAsync(x => x.Username == username);
    }
}