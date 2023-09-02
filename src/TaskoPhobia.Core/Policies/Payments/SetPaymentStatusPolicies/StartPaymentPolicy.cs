using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Policies.Payments.SetPaymentStatusPolicies;

internal sealed class StartPaymentPolicy : PolicyBase, ISetPaymentStatusPolicy
{
    public bool CanBeApplied(PaymentStatus newStatus)
    {
        return newStatus.Equals(PaymentStatus.Pending());
    }

    public void Apply(Payment payment)
    {
        payment.Start();
    }
}