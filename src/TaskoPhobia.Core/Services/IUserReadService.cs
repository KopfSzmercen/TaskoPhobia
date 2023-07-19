using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Services;

public interface IUserReadService
{
    Task<bool> ExistsByEmailAsync(Email email);
    Task<bool> ExistsByUsernameAsync(Username username);
    Task<User> GetByEmailAsync(Email email);
    Task<bool> ExistsByIdAsync(UserId id);
}