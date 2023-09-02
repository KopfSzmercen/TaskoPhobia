using TaskoPhobia.Core.Entities.Payments.ValueObjects.Exceptions;

namespace TaskoPhobia.Core.Entities.Payments.ValueObjects;

public record PaymentStatus
{
    public static readonly HashSet<string> AllowedValues = new() { "NEW", "PENDING", "CANCELED", "COMPLETED" };

    public PaymentStatus(string value)
    {
        if (!AllowedValues.Any(x => x.Equals(value))) throw new InvalidPaymentStatusException(value);
        Value = value;
    }

    public string Value { get; }

    public static PaymentStatus New()
    {
        return new PaymentStatus("NEW");
    }

    public static PaymentStatus Pending()
    {
        return new PaymentStatus("PENDING");
    }

    public static PaymentStatus Canceled()
    {
        return new PaymentStatus("CANCELED");
    }

    public static PaymentStatus Completed()
    {
        return new PaymentStatus("COMPLETED");
    }

    public static implicit operator string(PaymentStatus value)
    {
        return value.Value;
    }

    public static implicit operator PaymentStatus(string value)
    {
        return new PaymentStatus(value);
    }
}