using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}