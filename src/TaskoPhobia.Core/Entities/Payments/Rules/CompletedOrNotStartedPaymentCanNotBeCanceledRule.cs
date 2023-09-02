using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Payments.Rules;

internal sealed class CompletedOrNotStartedPaymentCanNotBeCanceledRule : IBusinessRule
{
    private readonly Payment _payment;

    public CompletedOrNotStartedPaymentCanNotBeCanceledRule(Payment payment)
    {
        _payment = payment;
    }

    public string Message => "Completed and not started payments can not be canceled";

    public bool IsBroken()
    {
        return _payment.IsCompleted() || _payment.IsNew();
    }
}