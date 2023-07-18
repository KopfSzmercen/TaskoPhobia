using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class InvitationCanNotBeRejectedException : CustomException
{
    public InvitationCanNotBeRejectedException() : base("Invitation can not be rejected.")
    {
    }
}