using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Policies.Payments.SetPaymentStatusPolicies;

internal sealed class CancelPaymentPolicy : PolicyBase, ISetPaymentStatusPolicy
{
    public bool CanBeApplied(PaymentStatus newStatus)
    {
        return newStatus.Equals(PaymentStatus.Canceled());
    }

    public void Apply(Payment payment)
    {
        payment.Cancel();
    }
}