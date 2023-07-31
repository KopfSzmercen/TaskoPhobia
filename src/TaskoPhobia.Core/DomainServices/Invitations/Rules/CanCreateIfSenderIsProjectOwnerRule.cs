using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.DomainServices.Invitations.Rules;

public class CanCreateIfSenderIsProjectOwnerRule : IBusinessRule
{
    private readonly Project _project;
    private readonly UserId _senderId;


    public CanCreateIfSenderIsProjectOwnerRule(Project project, UserId senderId)
    {
        _senderId = senderId;
        _project = project;
    }

    public string Message => "To invite to a project you must be its owner";

    public bool IsBroken()
    {
        return _project.OwnerId != _senderId;
    }
}