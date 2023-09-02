using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Payments.Rules;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Entities.Products.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money;
using TaskoPhobia.Shared.Abstractions.Time;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Unit.Entities;

public class PaymentTests : TestBase
{
    private readonly IClock _clock = new Clock();

    [Fact]
    public void Trying_To_Change_Completed_Payment_Should_Throw_Business_Rule_Exception()
    {
        var payment = Payment.CreateToVerify(Guid.NewGuid(), Guid.NewGuid(), _clock.DateTimeOffsetNow(),
            PaymentStatus.Completed(), "https://mock-url.pl", Money.Create(12, "PLN"));

        AssertBrokenRule<CompletedPaymentCanNotBeStartedRule>(() => payment.Start());

        AssertBrokenRule<CompletedOrNotStartedPaymentCanNotBeCanceledRule>(() => payment.Cancel());
    }

    [Fact]
    public void Trying_To_Init_Payment_For_Completed_Order_Should_Throw_Business_Rule_Exception()
    {
        var product = Product.CreateToVerify(Guid.NewGuid(), "name", Money.Create(12, "PLN"), "description");

        var order = Order.CreateToVerify(Guid.NewGuid(), product, Guid.NewGuid(), _clock.DateTimeOffsetNow(),
            OrderStatus.Completed());

        AssertBrokenRule<CanNotCreatePaymentForCompletedOrder>(() =>
            Payment.InitiatePayment(Guid.NewGuid(), order, _clock.DateTimeOffsetNow(), "https://mock-url.pl"));
    }

    [Fact]
    public void Trying_To_Cancel_Not_Started_Payment_Should_Throw_Business_Rule_Exception()
    {
        var payment = Payment.CreateToVerify(Guid.NewGuid(), Guid.NewGuid(), _clock.DateTimeOffsetNow(),
            PaymentStatus.New(), "https://mock-url.pl", Money.Create(12, "PLN"));

        AssertBrokenRule<CompletedOrNotStartedPaymentCanNotBeCanceledRule>(() => payment.Cancel());
    }

    [Fact]
    public void Trying_To_Complete_Not_Started_Payment_Should_Throw_Business_Rule_Exception()
    {
        var newPayment = Payment.CreateToVerify(Guid.NewGuid(), Guid.NewGuid(), _clock.DateTimeOffsetNow(),
            PaymentStatus.New(), "https://mock-url.pl", Money.Create(12, "PLN"));

        AssertBrokenRule<CanNotCompleteNotStartedPayment>(() => newPayment.Complete());

        var canceledPayment = Payment.CreateToVerify(Guid.NewGuid(), Guid.NewGuid(), _clock.DateTimeOffsetNow(),
            PaymentStatus.Canceled(), "https://mock-url.pl", Money.Create(12, "PLN"));

        AssertBrokenRule<CanNotCompleteNotStartedPayment>(() => newPayment.Complete());
    }
}