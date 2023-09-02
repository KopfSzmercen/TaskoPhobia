using TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Url;
using TaskoPhobia.Shared.Abstractions.Payments;

namespace TaskoPhobia.Infrastructure.Payments;

internal sealed class PaymentProcessor : IPaymentProcessor
{
    private const string MockedPaymentUrl = "https://payments/";
    private readonly PaymentOptions _paymentOptions;

    public PaymentProcessor(PaymentOptions options)
    {
        _paymentOptions = options;
    }

    public async Task<PaymentLinkDto> CreatePaymentLinkAsync(Guid orderId, Guid customerId, Guid paymentId, int price,
        string currency)
    {
        return new PaymentLinkDto(new Url($"{MockedPaymentUrl}{paymentId}"));
    }
}