using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Projects.Rules;

public class InvitationSenderMustBeProjectOwnerRule : IBusinessRule
{
    private readonly Invitation _invitation;
    private readonly Project _project;


    public InvitationSenderMustBeProjectOwnerRule(Project project, Invitation invitation)
    {
        _invitation = invitation;
        _project = project;
    }

    public string Message => "To invite to a project you must be its owner";

    public bool IsBroken()
    {
        return _project.OwnerId != _invitation.SenderId;
    }
}