using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Rules;

public class ReceiverMustNotParticipateProjectRule : IBusinessRule
{
    private readonly Invitation _invitation;
    private readonly Project _project;

    public ReceiverMustNotParticipateProjectRule(Project project, Invitation invitation)
    {
        _invitation = invitation;
        _project = project;
    }

    public string Message => "Can not invite user. User already participates the project.";

    public bool IsBroken()
    {
        return _project.Participations.Any(p => p.ParticipantId == _invitation.ReceiverId);
    }
}