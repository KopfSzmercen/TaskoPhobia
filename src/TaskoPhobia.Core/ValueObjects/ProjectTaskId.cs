using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record ProjectTaskId
{
    public ProjectTaskId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidEntityIdException(value);
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(ProjectTaskId value)
    {
        return value.Value;
    }

    public static implicit operator ProjectTaskId(Guid value)
    {
        return new ProjectTaskId(value);
    }
}