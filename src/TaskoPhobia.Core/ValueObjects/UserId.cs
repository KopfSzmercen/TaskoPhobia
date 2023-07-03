﻿using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record UserId
{
    public UserId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidEntityIdException(value);

        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(UserId value)
    {
        return value.Value;
    }

    public static implicit operator UserId(Guid value)
    {
        return new UserId(value);
    }

    public static bool operator ==(UserId userId, Guid guid) => userId!.Value == guid;
    public static bool operator !=(UserId userId, Guid guid) => !(userId == guid);
}