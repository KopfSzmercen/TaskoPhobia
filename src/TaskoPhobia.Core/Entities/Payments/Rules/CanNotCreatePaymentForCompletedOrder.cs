using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Payments.Rules;

internal sealed class CanNotCreatePaymentForCompletedOrder : IBusinessRule
{
    private readonly Order _order;

    public CanNotCreatePaymentForCompletedOrder(Order order)
    {
        _order = order;
    }

    public string Message => "Can't init payment for a completed order";

    public bool IsBroken()
    {
        return _order.IsCompleted();
    }
}