using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public sealed record TaskAssignmentsLimit
{
    private const ushort MinAllowedNumberOfTaskAssignments = 1;
    private const ushort MaxAllowedNumberOfTaskAssignments = 10;

    public TaskAssignmentsLimit(int value)
    {
        if (value is < MinAllowedNumberOfTaskAssignments or > MaxAllowedNumberOfTaskAssignments)
            throw new InvalidTaskAssignmentsLimit(MinAllowedNumberOfTaskAssignments, MaxAllowedNumberOfTaskAssignments);

        Value = value;
    }

    public int Value { get; }

    public static implicit operator TaskAssignmentsLimit(int value)
    {
        return new TaskAssignmentsLimit(value);
    }

    public static implicit operator int(TaskAssignmentsLimit value)
    {
        return value.Value;
    }
}