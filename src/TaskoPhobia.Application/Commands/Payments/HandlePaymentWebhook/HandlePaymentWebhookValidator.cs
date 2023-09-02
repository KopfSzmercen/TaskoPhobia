using FluentValidation;
using TaskoPhobia.Core.Entities.Payments.ValueObjects;

namespace TaskoPhobia.Application.Commands.Payments.HandlePaymentWebhook;

internal class HandlePaymentWebhookValidator : AbstractValidator<HandlePaymentWebhook>
{
    public HandlePaymentWebhookValidator()
    {
        RuleFor(x => x.PaymentId)
            .NotEmpty();

        RuleFor(x => x.PaymentStatus)
            .NotEmpty()
            .Must(BeValidPaymentStatus)
            .WithMessage($"Payment status must be one of {string.Join(",", PaymentStatus.AllowedValues)}");
    }

    private bool BeValidPaymentStatus(string paymentStatus)
    {
        return PaymentStatus.AllowedValues.Any(x => x.Equals(paymentStatus));
    }
}