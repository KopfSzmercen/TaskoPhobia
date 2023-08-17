using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record ProjectName
{
    public ProjectName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 30 or < 3)
            throw new InvalidProjectNameException(value);

        Value = value;
    }

    public string Value { get; }
    public static implicit operator ProjectName(string value)
    {
        return new ProjectName(value);
    }

    public static implicit operator string(ProjectName value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return Value;
    }
}