using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Payments.Rules;

internal sealed class CompletedPaymentCanNotBeStartedRule : IBusinessRule
{
    private readonly Payment _payment;

    public CompletedPaymentCanNotBeStartedRule(Payment payment)
    {
        _payment = payment;
    }

    public string Message => "Can't change status of a completed payment";

    public bool IsBroken()
    {
        return _payment.IsCompleted();
    }
}