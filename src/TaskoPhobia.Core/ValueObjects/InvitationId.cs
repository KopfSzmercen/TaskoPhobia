using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record InvitationId
{
    public InvitationId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidEntityIdException(value);
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(InvitationId value)
    {
        return value.Value;
    }

    public static implicit operator InvitationId(Guid value)
    {
        return new InvitationId(value);
    }
}