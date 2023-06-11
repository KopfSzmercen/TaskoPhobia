using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record ProjectTaskName
{
    public string Value { get; }

    public ProjectTaskName(string value )
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 30 or < 3)
        {
            throw new InvalidProjectTaskNameException(value);
        }
        Value = value;
    }

    public static implicit operator ProjectTaskName(string value) => new(value);
    public static implicit operator string(ProjectTaskName value) => value.Value;
    public override string ToString() => Value;
};