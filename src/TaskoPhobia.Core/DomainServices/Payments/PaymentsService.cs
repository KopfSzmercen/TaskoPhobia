using TaskoPhobia.Core.DomainServices.Payments.Exceptions;
using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Core.Policies.Payments.SetPaymentStatusPolicies;

namespace TaskoPhobia.Core.DomainServices.Payments;

internal sealed class PaymentsService : IPaymentsService
{
    private readonly IEnumerable<ISetPaymentStatusPolicy> _setPaymentStatusPolicies;

    public PaymentsService(IEnumerable<ISetPaymentStatusPolicy> setPaymentStatusPolicies)
    {
        _setPaymentStatusPolicies = setPaymentStatusPolicies;
    }

    public void UpdatePaymentStatus(Payment payment, PaymentStatus newStatus)
    {
        var applicablePolicy = _setPaymentStatusPolicies
            .SingleOrDefault(x => x.CanBeApplied(newStatus));

        if (applicablePolicy is null) throw new NoSetPaymentStatusPolicyFoundException(newStatus);

        applicablePolicy.Apply(payment);
    }
}