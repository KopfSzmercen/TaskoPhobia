using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Policies.Invitations.Exceptions;

public class CanNotSendMoreInvitationsException : CustomException
{
    public CanNotSendMoreInvitationsException() : base("User blocked sending more invitations for this project.")
    {
    }
}