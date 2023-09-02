using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Payments.Rules;

internal sealed class CanNotCompleteNotStartedPayment : IBusinessRule
{
    private readonly Payment _payment;

    public CanNotCompleteNotStartedPayment(Payment payment)
    {
        _payment = payment;
    }

    public string Message => "Only started payments can be completed";

    public bool IsBroken()
    {
        return _payment.IsPending() is false;
    }
}