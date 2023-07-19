using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.Users.Rules;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Users;

public class User : Entity
{
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
    public ICollection<Project> OwnedProjects { get; } = new List<Project>();
    public IEnumerable<Invitation> SentInvitations { get; init; } = new List<Invitation>();
    public IEnumerable<Invitation> ReceivedInvitations { get; init; } = new List<Invitation>();

    public IEnumerable<ProjectParticipation> ProjectParticipations { get; init; } = new List<ProjectParticipation>();

    public void AddProject(Project project)
    {
        CheckRule(new LimitOfOwnedProjectsForFreeAccountIsNotExceededRule(this));
        CheckRule(new LimitOfOwnedProjectsForBasicAccountIsNotExceededRule(this));

        OwnedProjects.Add(project);
    }
}