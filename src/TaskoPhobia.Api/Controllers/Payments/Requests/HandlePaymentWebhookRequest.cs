using Microsoft.AspNetCore.Mvc;
using TaskoPhobia.Application.Commands.Payments.HandlePaymentWebhook;

namespace TaskoPhobia.Api.Controllers.Payments.Requests;

public record struct PaymentData
{
    public string PaymentStatus { get; set; }
}

public class HandlePaymentWebhookRequest
{
    [FromBody] public PaymentData PaymentData { get; init; }
    [FromRoute(Name = "paymentId")] public Guid PaymentId { get; init; }

    public HandlePaymentWebhook ToCommand()
    {
        return new HandlePaymentWebhook(PaymentId, PaymentData.PaymentStatus);
    }
}