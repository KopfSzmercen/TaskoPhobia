using TaskoPhobia.Core.DomainServices.Payments.Exceptions;
using TaskoPhobia.Core.DomainServices.Payments.Rules;
using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Policies.Payments.SetPaymentStatusPolicies;
using TaskoPhobia.Shared.Abstractions.Domain;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Url;

namespace TaskoPhobia.Core.DomainServices.Payments;

internal sealed class PaymentsService : DomainService, IPaymentsService
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

    public Payment CreatePaymentForOrder(Order order, PaymentId paymentId, DateTimeOffset now, Url redirectUrl)
    {
        CheckRule(new CanNotCreatePaymentForCompletedOrderRule(order));

        return Payment.New(paymentId, order.Id, order.Price.Copy(), now, redirectUrl);
    }
}