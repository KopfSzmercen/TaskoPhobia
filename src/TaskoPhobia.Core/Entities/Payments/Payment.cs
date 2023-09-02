#nullable enable
using TaskoPhobia.Core.Entities.Payments.Events;
using TaskoPhobia.Core.Entities.Payments.Rules;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Core.Entities.Products;
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

    private Payment()
    {
    }

    public PaymentId Id { get; }
    public PaymentStatus Status { get; private set; }
    public DateTimeOffset? PaidAt { get; private set; }
    public Url? RedirectUrl { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public OrderId OrderId { get; }
    public Money MoneyToPay { get; }

    public static Payment InitiatePayment(PaymentId id, Order order, DateTimeOffset now, Url redirectUrl)
    {
        CheckRule(new CanNotCreatePaymentForCompletedOrder(order));
        return new Payment(id, order.Id, now, PaymentStatus.New(), redirectUrl,
            Money.Create(order.Price.Amount, order.Price.Currency));
    }

    public void RefreshUrl(Url url)
    {
        CheckRule(new UrlCanBeRefreshedRule(this));
        RedirectUrl = url;
        Status = PaymentStatus.Pending();
    }

    public bool IsCanceled()
    {
        return Status.Equals(PaymentStatus.Canceled());
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

    public static Payment CreateToVerify(PaymentId id, OrderId orderId, DateTimeOffset now, PaymentStatus status,
        Url redirectUrl,
        Money moneyToPay)
    {
        return new Payment(id, orderId, now, status, redirectUrl,
            moneyToPay);
    }
}