using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.BaseId;

namespace TaskoPhobia.Core.Entities.Payments.ValueObjects;

public sealed class PaymentId : BaseId
{
    public PaymentId(Guid value) : base(value)
    {
    }

    public static implicit operator Guid(PaymentId value)
    {
        return value.Value;
    }

    public static implicit operator PaymentId(Guid value)
    {
        return new PaymentId(value);
    }
}