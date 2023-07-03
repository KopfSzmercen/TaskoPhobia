using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record Password
{
    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 200 or < 5) throw new InvalidPasswordException();

        Value = value;
    }

    public string Value { get; }

    public static implicit operator Password(string value)
    {
        return new Password(value);
    }

    public static implicit operator string(Password value)
    {
        return value?.Value;
    }

    public override string ToString()
    {
        return Value;
    }
}