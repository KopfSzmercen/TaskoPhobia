using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.DomainServices.Payments.Rules;

internal sealed class CanNotCreatePaymentForCompletedOrderRule : IBusinessRule
{
    private readonly Order _order;

    public CanNotCreatePaymentForCompletedOrderRule(Order order)
    {
        _order = order;
    }

    public string Message => "Can't init payment for a completed order";

    public bool IsBroken()
    {
        return _order.IsCompleted();
    }
}