using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Tests.Integration;

internal class TestUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public Task<User> FindByIdAsync(UserId id)
    {
        return Task.FromResult(_users.SingleOrDefault(x => x.Id == id));
    }

    public Task AddAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user)
    {
        var userIndex = _users.FindIndex(x => x.Id == user.Id);
        _users[userIndex] = user;
        return Task.CompletedTask;
    }


    public Task<User> GetByEmailAsync(Email email)
    {
        return Task.FromResult(_users.SingleOrDefault(x => x.Email == email));
    }

    public Task<User> GetByUsernameAsync(Username username)
    {
        return Task.FromResult(_users.SingleOrDefault(x => x.Username == username));
    }
}