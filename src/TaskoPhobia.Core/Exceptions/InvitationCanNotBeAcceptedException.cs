using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class InvitationCanNotBeAcceptedException : CustomException
{
    public InvitationCanNotBeAcceptedException() : base("Invitation can not be accepted")
    {
    }
}