using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record TaskAssignmentId
{
    public TaskAssignmentId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidEntityIdException(value);
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(TaskAssignmentId value)
    {
        return value.Value;
    }

    public static implicit operator TaskAssignmentId(Guid value)
    {
        return new TaskAssignmentId(value);
    }
}