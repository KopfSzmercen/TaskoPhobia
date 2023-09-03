#nullable enable
using TaskoPhobia.Core.Entities.Payments.Events;
using TaskoPhobia.Core.Entities.Payments.Rules;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Core.Entities.Products.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Url;

namespace TaskoPhobia.Core.Entities.Payments;

public class Payment : Entity
{
    private Payment(PaymentId id, OrderId orderId, DateTimeOffset now, PaymentStatus status, Url redirectUrl,
        Money moneyToPay)
    {
        Id = id;
        CreatedAt = now;
        Status = status;
        RedirectUrl = redirectUrl;
        OrderId = orderId;
        MoneyToPay = moneyToPay;
    }

    public Payment()
    {
    }

    public PaymentId Id { get; }
    public PaymentStatus Status { get; private set; }
    public DateTimeOffset? PaidAt { get; private set; }
    public Url? RedirectUrl { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public OrderId OrderId { get; init; }
    public Money MoneyToPay { get; }

    internal static Payment New(PaymentId id, OrderId orderId, Money price, DateTimeOffset now, Url redirectUrl)
    {
        return new Payment(id, orderId, now, PaymentStatus.New(), redirectUrl, price);
    }

    public void RefreshUrl(Url url)
    {
        CheckRule(new CanNotRefreshUrlForCompletedOrPendingPayment(this));
        RedirectUrl = url;
        Status = PaymentStatus.Pending();
    }

    public bool IsNew()
    {
        return Status.Equals(PaymentStatus.New());
    }

    public bool IsCompleted()
    {
        return Status.Equals(PaymentStatus.Completed());
    }

    public bool IsPending()
    {
        return Status.Equals(PaymentStatus.Pending());
    }

    internal void Start()
    {
        CheckRule(new CompletedPaymentCanNotBeStartedRule(this));
        Status = PaymentStatus.Pending();

        AddDomainEvent(new PaymentStartedDomainEvent(Id));
    }

    internal void Cancel()
    {
        CheckRule(new CompletedOrNotStartedPaymentCanNotBeCanceledRule(this));
        Status = PaymentStatus.Canceled();

        AddDomainEvent(new PaymentCanceledDomainEvent(Id));
    }

    internal void Complete()
    {
        CheckRule(new CanNotCompleteNotStartedPayment(this));
        Status = PaymentStatus.Completed();
        PaidAt = DateTimeOffset.Now;

        AddDomainEvent(new PaymentCompletedDomainEvent(Id, OrderId));
    }
}