using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Url;

namespace TaskoPhobia.Core.DomainServices.Payments;

public interface IPaymentsService
{
    void UpdatePaymentStatus(Payment payment, PaymentStatus newStatus);
    Payment CreatePaymentForOrder(Order order, PaymentId paymentId, DateTimeOffset now, Url redirectUrl);
}