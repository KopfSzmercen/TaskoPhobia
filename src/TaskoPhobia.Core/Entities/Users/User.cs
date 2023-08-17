using TaskoPhobia.Core.Entities.Users.Events;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Users;

public class User : Entity
{
    private User(UserId id, Email email, Username username, Password password, Role role, DateTime createdAt,
        AccountType accountType)
    {
        Id = id;
        Email = email;
        Username = username;
        Password = password;
        Role = role;
        CreatedAt = createdAt;
        AccountType = accountType;

        AddDomainEvent(new UserRegisteredDomainEvent(id, email));
    }

    public User()
    {
    }

    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public AccountType AccountType { get; }

    public static User New(UserId id, Email email, Username username, Password password, DateTime createdAt)
    {
        return new User(id, email, username, password, Role.User(), createdAt, AccountType.Basic());
    }
}