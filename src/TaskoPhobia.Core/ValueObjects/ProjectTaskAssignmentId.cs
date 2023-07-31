using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record ProjectTaskAssignmentId
{
    public ProjectTaskAssignmentId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidEntityIdException(value);
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(ProjectTaskAssignmentId value)
    {
        return value.Value;
    }

    public static implicit operator ProjectTaskAssignmentId(Guid value)
    {
        return new ProjectTaskAssignmentId(value);
    }
}