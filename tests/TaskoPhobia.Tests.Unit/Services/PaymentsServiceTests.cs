using Shouldly;
using TaskoPhobia.Core.DomainServices.Payments;
using TaskoPhobia.Core.DomainServices.Payments.Exceptions;
using TaskoPhobia.Core.DomainServices.Payments.Rules;
using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Policies.Payments.SetPaymentStatusPolicies;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money;
using Xunit;

namespace TaskoPhobia.Tests.Unit.Services;

public class PaymentsServiceTests : TestBase
{
    private readonly PaymentsService _sut = new(new List<ISetPaymentStatusPolicy>
    {
        new CancelPaymentPolicy(),
        new CompletePaymentPolicy(),
        new StartPaymentPolicy()
    });

    [Fact]
    public void
        Trying_To_Create_Payment_For_Completed_Order_Should_Throw_CanNotCreatePaymentForCompletedOrderRule_Exception()
    {
        var order = Order.New(Guid.NewGuid(), Guid.NewGuid(),
            Money.Create(12, "PLN"), Guid.NewGuid(), DateTimeOffset.Now);
        order.Complete();

        AssertBrokenRule<CanNotCreatePaymentForCompletedOrderRule>(() => _sut.CreatePaymentForOrder(order,
            Guid.NewGuid(), DateTimeOffset.Now, "https://url.pl"));
    }

    [Fact]
    public void CreatePaymentForOrder_ShouldReturnPayment_IfDataIsCorrect
        ()
    {
        var order = Order.New(Guid.NewGuid(), Guid.NewGuid(),
            Money.Create(12, "PLN"), Guid.NewGuid(), DateTimeOffset.Now);

        var result = _sut.CreatePaymentForOrder(order, Guid.NewGuid(),
            DateTimeOffset.Now, "https://url.pl");

        result.ShouldNotBeNull();
        result.ShouldBeOfType<Payment>();
    }

    [Fact]
    public void
        UpdatePaymentStatus_ShouldThrow_NoSetPaymentStatusPolicyFoundException_IfThereIsNotPolicyForPaymentStatus()
    {
        var payment = Payment.New(Guid.NewGuid(), Guid.NewGuid(), Money.Create(12, "PLN"),
            DateTimeOffset.Now, "https://url.pl");

        Should.Throw<NoSetPaymentStatusPolicyFoundException>(() =>
            _sut.UpdatePaymentStatus(payment, PaymentStatus.New()));
    }
}