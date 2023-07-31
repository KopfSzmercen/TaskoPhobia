using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.DomainServices.Invitations.Rules;

public class CanCreateIfReceiverNotParticipateProjectRule : IBusinessRule
{
    private readonly bool _receiverParticipatesProject;

    public CanCreateIfReceiverNotParticipateProjectRule(bool receiverParticipatesProject)
    {
        _receiverParticipatesProject = receiverParticipatesProject;
    }

    public string Message => "Can not invite user. User already participates the project.";

    public bool IsBroken()
    {
        return _receiverParticipatesProject;
    }
}