using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public class NoInvitationPolicyFoundException : CustomException
{
    public NoInvitationPolicyFoundException() : base("No invitation policy found.")
    {
    }
}