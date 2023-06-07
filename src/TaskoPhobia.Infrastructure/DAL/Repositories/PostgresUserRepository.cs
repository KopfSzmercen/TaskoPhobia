using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Infrastructure.DAL.Repositories;

internal sealed class PostgresUserRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    private readonly TaskoPhobiaDbContext _dbContext;
    
    public PostgresUserRepository(TaskoPhobiaDbContext dbContext, TaskoPhobiaDbContext dbContext1)
    {
        _dbContext = dbContext1;
        _users = dbContext.Users;
    }
    
    public Task<User> GetByIdAsync(UserId id)
    {
        return _users.SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task<User> GetByEmailAsync(Email email)
    {
        return _users.SingleOrDefaultAsync(x => x.Email == email);
    }

    public Task<User> GetByUsernameAsync(Username username)
    {
        return _users.SingleOrDefaultAsync(x => x.Username == username);
    }

    public async Task AddAsync(User user)
    {
         _users.Add(user);
         await _dbContext.SaveChangesAsync();
    }
}
