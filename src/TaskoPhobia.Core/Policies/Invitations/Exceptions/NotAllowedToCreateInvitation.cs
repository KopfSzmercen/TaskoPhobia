using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Policies.Invitations.Exceptions;

public class NotAllowedToCreateInvitation : CustomException
{
    public NotAllowedToCreateInvitation() : base("You are not allowed to invite to project unless you are its owner.")
    {
    }
}