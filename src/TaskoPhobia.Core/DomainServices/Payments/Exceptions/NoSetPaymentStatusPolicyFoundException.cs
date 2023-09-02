using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Exceptions;

namespace TaskoPhobia.Core.DomainServices.Payments.Exceptions;

public class NoSetPaymentStatusPolicyFoundException : CustomException
{
    public NoSetPaymentStatusPolicyFoundException(PaymentStatus paymentStatus) : base(
        $"No policy found for setting payment status to {paymentStatus.Value}")
    {
    }
}