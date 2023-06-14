using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public record ProjectDescription
{
    public ProjectDescription(string value)
    {
        if (value.Length is > 1000) throw new InvalidProjectDescriptionException();
        Value = value;
    }

    public string Value { get; }

    public static implicit operator ProjectDescription(string value)
    {
        return new ProjectDescription(value);
    }

    public static implicit operator string(ProjectDescription value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return Value;
    }
}