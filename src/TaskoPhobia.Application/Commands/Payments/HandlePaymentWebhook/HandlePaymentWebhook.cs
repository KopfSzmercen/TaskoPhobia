using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Payments.HandlePaymentWebhook;

public record HandlePaymentWebhook(Guid PaymentId, string PaymentStatus) : ICommand;