using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;

namespace TaskoPhobia.Core.Policies.Payments.SetPaymentStatusPolicies;

internal interface ISetPaymentStatusPolicy
{
    bool CanBeApplied(PaymentStatus newStatus);
    void Apply(Payment payment);
}