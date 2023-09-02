using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;

namespace TaskoPhobia.Core.DomainServices.Payments;

public interface IPaymentsService
{
    void UpdatePaymentStatus(Payment payment, PaymentStatus newStatus);
}