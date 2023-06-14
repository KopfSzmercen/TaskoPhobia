using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record ProjectTaskName
{
    public ProjectTaskName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 30 or < 3)
            throw new InvalidProjectTaskNameException(value);
        Value = value;
    }

    public string Value { get; }

    public static implicit operator ProjectTaskName(string value)
    {
        return new ProjectTaskName(value);
    }

    public static implicit operator string(ProjectTaskName value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return Value;
    }
}