using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record ProjectId
{
    public ProjectId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidEntityIdException(value);

        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator ProjectId(string value)
    {
        return Guid.Parse(value);
    }

    public static implicit operator Guid(ProjectId value)
    {
        return value.Value;
    }

    public static implicit operator ProjectId(Guid value)
    {
        return new ProjectId(value);
    }
}