using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.DomainServices.Payments;
using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Payments.HandlePaymentWebhook;

internal sealed class HandlePaymentWebhookHandler : ICommandHandler<HandlePaymentWebhook>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentsService _paymentsService;

    public HandlePaymentWebhookHandler(IPaymentRepository paymentRepository, IPaymentsService paymentsService)
    {
        _paymentRepository = paymentRepository;
        _paymentsService = paymentsService;
    }

    public async Task HandleAsync(HandlePaymentWebhook command)
    {
        var payment = await _paymentRepository.FindByIdAsync(command.PaymentId);

        if (payment is null) throw new PaymentNotFoundException();

        _paymentsService.UpdatePaymentStatus(payment, command.PaymentStatus);

        await _paymentRepository.UpdateAsync(payment);
    }
}