using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record ProjectTaskId
{
    public Guid Value { get; }

    public ProjectTaskId(Guid value)
    {
        if (value == Guid.Empty)   throw new InvalidEntityIdException(value);
        Value = value;
    }

    public static implicit operator Guid(ProjectTaskId value) => value.Value;
    
    public static implicit operator ProjectTaskId(Guid value) => new(value);
}