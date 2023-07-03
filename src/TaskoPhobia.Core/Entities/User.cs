using TaskoPhobia.Core.Exceptions;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.Entities;

public class User
{
    private const ushort MaxNumOfProjectsForFreeAccount = 3;
    private const ushort MaxNumOfProjectsForBasicAccount = 6;

    public User(UserId id, Email email, Username username, Password password, Role role, DateTime createdAt,
        AccountType accountType)
    {
        Id = id;
        Email = email;
        Username = username;
        Password = password;
        Role = role;
        CreatedAt = createdAt;
        AccountType = accountType;
    }

    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public AccountType AccountType { get; }
    public ICollection<Project> OwnedProjects { get; } = new List<Project>();

    public void AddProject(Project project)
    {
        if (AccountType.Equals(AccountType.Free()) && OwnedProjects.Count + 1 > MaxNumOfProjectsForFreeAccount)
            throw new NotAllowedToCreateMoreProjectsException();

        if (AccountType.Equals(AccountType.Basic()) && OwnedProjects.Count + 1 > MaxNumOfProjectsForBasicAccount)
            throw new NotAllowedToCreateMoreProjectsException();

        OwnedProjects.Add(project);
    }
}