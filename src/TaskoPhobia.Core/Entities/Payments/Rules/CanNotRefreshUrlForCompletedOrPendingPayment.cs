using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Payments.Rules;

public class CanNotRefreshUrlForCompletedOrPendingPayment : IBusinessRule
{
    private readonly Payment _payment;

    public CanNotRefreshUrlForCompletedOrPendingPayment(Payment payment)
    {
        _payment = payment;
    }

    public string Message => "Can't refresh payment url";

    public bool IsBroken()
    {
        return _payment.IsCompleted() || _payment.IsPending();
    }
}