using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Exceptions;

public sealed class InvalidTaskAssignmentsLimit : CustomException
{
    public InvalidTaskAssignmentsLimit(int minAllowedValue, int maxAllowedValue) : base(
        $"Task assignments limit must be between {minAllowedValue} and {maxAllowedValue}")
    {
    }
}