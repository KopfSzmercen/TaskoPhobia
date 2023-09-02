using TaskoPhobia.Shared.Abstractions.Payments;

namespace TaskoPhobia.Application.Commands.Payments.CreatePaymentLink;

public interface IPaymentLinkStorage
{
    void Set(PaymentLinkDto paymentLink);
    PaymentLinkDto Get();
}