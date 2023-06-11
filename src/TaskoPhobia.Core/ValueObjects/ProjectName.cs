using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record ProjectName
{
    public string Value { get; }

    public ProjectName(string value )
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 30 or < 3)
        {
            throw new InvalidProjectNameException(value);
        }
            
        Value = value;
    }

    public static implicit operator ProjectName(string value) => new(value);
    public static implicit operator string(ProjectName value) => value.Value;
    public override string ToString() => Value;
};