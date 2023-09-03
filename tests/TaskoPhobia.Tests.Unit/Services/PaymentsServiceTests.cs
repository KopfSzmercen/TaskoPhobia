using TaskoPhobia.Core.DomainServices.Payments;
using TaskoPhobia.Core.DomainServices.Payments.Rules;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Policies.Payments.SetPaymentStatusPolicies;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Money;
using Xunit;

namespace TaskoPhobia.Tests.Unit.Services;

public class PaymentsServiceTests : TestBase
{
    [Fact]
    public void
        Trying_To_Create_Payment_For_Completed_Order_Should_Throw_CanNotCreatePaymentForCompletedOrderRule_Exception()
    {
        var order = Order.New(Guid.NewGuid(), Guid.NewGuid(),
            Money.Create(12, "PLN"), Guid.NewGuid(), DateTimeOffset.Now);
        order.Complete();

        var service = new PaymentsService(new List<ISetPaymentStatusPolicy>());

        AssertBrokenRule<CanNotCreatePaymentForCompletedOrderRule>(() => service.CreatePaymentForOrder(order,
            Guid.NewGuid(), DateTimeOffset.Now, "https://url.pl"));
    }
}