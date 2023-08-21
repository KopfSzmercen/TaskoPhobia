using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.Entities.Payments.ValueObjects.Exceptions;

public sealed class InvalidPaymentStatusException : CustomException
{
    public InvalidPaymentStatusException(string value) : base($"{value} is not a valid payment status.")
    {
    }
}