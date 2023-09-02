using Microsoft.AspNetCore.Http;
using TaskoPhobia.Application.Commands.Payments.CreatePaymentLink;
using TaskoPhobia.Shared.Abstractions.Payments;

namespace TaskoPhobia.Infrastructure.HttpContextStorage;

internal sealed class HttpContextPaymentLinkStorage : IPaymentLinkStorage
{
    private const string PaymentLinkKey = "payment_link";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextPaymentLinkStorage(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Set(PaymentLinkDto paymentLink)
    {
        _httpContextAccessor.HttpContext?.Items.TryAdd(PaymentLinkKey, paymentLink);
    }

    public PaymentLinkDto Get()
    {
        if (_httpContextAccessor is null) return null;
        if (_httpContextAccessor.HttpContext != null &&
            _httpContextAccessor.HttpContext.Items.TryGetValue(PaymentLinkKey, out var paymentLink))
            return paymentLink as PaymentLinkDto;

        return null;
    }
}