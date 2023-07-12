using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Application.Exceptions;

public class InvitationNotFoundException : CustomException
{
    public InvitationNotFoundException(InvitationId id) : base($"Invitation {id.Value} not found")
    {
    }
}