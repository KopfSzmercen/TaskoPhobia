using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public record ProjectDescription
{
    public string Value { get; }

    public ProjectDescription(string value )
    {
        if (value.Length is > 1000) throw new InvalidProjectDescriptionException();
        Value = value;
    }

    public static implicit operator ProjectDescription(string value) => new(value);
    public static implicit operator string(ProjectDescription value) => value.Value;
    public override string ToString() => Value;
}