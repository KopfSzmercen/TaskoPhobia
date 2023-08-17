using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.BaseId.Exceptions;

namespace TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.BaseId;

public class BaseId
{
    protected BaseId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidBaseIdException(value);

        Value = value;
    }

    public Guid Value { get; }
}