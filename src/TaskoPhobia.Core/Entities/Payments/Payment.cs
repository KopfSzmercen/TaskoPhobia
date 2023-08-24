using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Url;

namespace TaskoPhobia.Core.Entities.Payments;

public class Payment
{
    public PaymentId Id { get; }
    public PaymentStatus Status { get; private set; }
    public DateTimeOffset? PaidAt { get; private set; }
    public Url? RedirectUrl { get; private set; }


    public void StartPayment(Url redirectUrl)
    {
        RedirectUrl = redirectUrl;
        Status = PaymentStatus.Pending();
    }

    public void CompletePayment(DateTimeOffset paidAt)
    {
        PaidAt = paidAt;
        Status = PaymentStatus.Completed();
    }
}