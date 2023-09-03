using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Payments.Rules;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money;
using TaskoPhobia.Shared.Abstractions.Time;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Unit.Entities;

public class PaymentTests : TestBase
{
    private readonly IClock _clock = new Clock();

    #region Setup

    private Payment CreatePayment()
    {
        return Payment.New(Guid.NewGuid(), Guid.NewGuid(), Money.Create(12, "PLN"), _clock.DateTimeOffsetNow(),
            "https://mock-url.pl");
    }

    #endregion

    [Fact]
    public void Trying_To_Change_Completed_Payment_Should_Throw_Business_Rule_Exception()
    {
        var payment = CreatePayment();
        payment.Start();
        payment.Complete();

        AssertBrokenRule<CompletedPaymentCanNotBeStartedRule>(() => payment.Start());

        AssertBrokenRule<CompletedOrNotStartedPaymentCanNotBeCanceledRule>(() => payment.Cancel());
    }

    [Fact]
    public void Trying_To_Cancel_Not_Started_Payment_Should_Throw_Business_Rule_Exception()
    {
        var payment = CreatePayment();
        AssertBrokenRule<CompletedOrNotStartedPaymentCanNotBeCanceledRule>(() => payment.Cancel());
    }

    [Fact]
    public void Trying_To_Complete_Not_Started_Payment_Should_Throw_Business_Rule_Exception()
    {
        var payment = CreatePayment();

        AssertBrokenRule<CanNotCompleteNotStartedPayment>(() => payment.Complete());

        payment.Start();
        payment.Cancel();

        AssertBrokenRule<CanNotCompleteNotStartedPayment>(() => payment.Complete());
    }

    [Fact]
    public void Trying_To_Refresh_Url_If_Payment_Is_Completed_Or_Pending_ShouldFail()
    {
        var paymentWithCompletedStatus = CreatePayment();
        paymentWithCompletedStatus.Start();
        paymentWithCompletedStatus.Complete();

        AssertBrokenRule<CanNotRefreshUrlForCompletedOrPendingPayment>(() =>
            paymentWithCompletedStatus.RefreshUrl("https://url.pl"));

        var paymentWithPendingStatus = CreatePayment();
        paymentWithPendingStatus.Start();

        AssertBrokenRule<CanNotRefreshUrlForCompletedOrPendingPayment>(() =>
            paymentWithCompletedStatus.RefreshUrl("https://url.pl"));
    }
}