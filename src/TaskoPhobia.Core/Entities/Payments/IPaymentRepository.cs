using TaskoPhobia.Core.Entities.Payments.ValueObjects;
using TaskoPhobia.Core.Entities.Products.ValueObjects;

namespace TaskoPhobia.Core.Entities.Payments;

public interface IPaymentRepository
{
    Task AddAsync(Payment payment);
    Task<Payment> FindByIdAsync(PaymentId paymentId);
    Task<Payment> FindByOrderIdAsync(OrderId orderId);
    Task UpdateAsync(Payment payment);
}