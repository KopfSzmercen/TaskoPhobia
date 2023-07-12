using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record ProjectParticipationId
{
    public ProjectParticipationId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidEntityIdException(value);
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(ProjectParticipationId value)
    {
        return value.Value;
    }

    public static implicit operator ProjectParticipationId(Guid value)
    {
        return new ProjectParticipationId(value);
    }
}