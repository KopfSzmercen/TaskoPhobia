using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record InvitationTitle
{
    public InvitationTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidInvitationTitleException();

        Value = value;
    }

    public string Value { get; }

    public static implicit operator InvitationTitle(string value)
    {
        return new InvitationTitle(value);
    }

    public static implicit operator string(InvitationTitle value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return Value;
    }
}