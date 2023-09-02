using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Entities.Payments;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Contexts;
using TaskoPhobia.Shared.Abstractions.Payments;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Application.Commands.Payments.CreatePaymentLink;

internal sealed class CreatePaymentLinkHandler : ICommandHandler<CreatePaymentLink>
{
    private readonly IClock _clock;
    private readonly IContext _context;
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentLinkStorage _paymentLinkStorage;
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly IPaymentRepository _paymentRepository;

    public CreatePaymentLinkHandler(IOrderRepository orderRepository, IContext context, IClock clock,
        IPaymentRepository paymentRepository, IPaymentLinkStorage paymentLinkStorage,
        IPaymentProcessor paymentProcessor)
    {
        _orderRepository = orderRepository;
        _context = context;
        _clock = clock;
        _paymentRepository = paymentRepository;
        _paymentLinkStorage = paymentLinkStorage;
        _paymentProcessor = paymentProcessor;
    }

    public async Task HandleAsync(CreatePaymentLink command)
    {
        var order = await _orderRepository.FindByIdAsync(command.OrderId);

        if (order is null || order?.CustomerId != _context.Identity.Id) throw new OrderNotFound();

        var payment = await _paymentRepository.FindByOrderIdAsync(command.OrderId);

        if (payment is null)
        {
            await InitiatePaymentAndCreateLink(order);
            return;
        }

        if (payment.IsPending() && payment.RedirectUrl is not null)
        {
            _paymentLinkStorage.Set(new PaymentLinkDto(payment.RedirectUrl));
            return;
        }

        await RefreshPaymentLink(payment, order);
    }

    private async Task RefreshPaymentLink(Payment payment, Order order)
    {
        var refreshedPaymentLinkDto = await _paymentProcessor.CreatePaymentLinkAsync(order.Id, order.CustomerId,
            payment.Id,
            order.Price.Amount, order.Price.Currency);

        payment.RefreshUrl(refreshedPaymentLinkDto.PaymentLink);

        await _paymentRepository.UpdateAsync(payment);

        _paymentLinkStorage.Set(refreshedPaymentLinkDto);
    }

    private async Task InitiatePaymentAndCreateLink(Order order)
    {
        var newPaymentId = Guid.NewGuid();

        var paymentLinkDto = await _paymentProcessor.CreatePaymentLinkAsync(order.Id, order.CustomerId,
            newPaymentId,
            order.Price.Amount, order.Price.Currency);

        var newPayment = Payment.InitiatePayment(newPaymentId, order, _clock.DateTimeOffsetNow(),
            paymentLinkDto.PaymentLink);

        await _paymentRepository.AddAsync(newPayment);

        _paymentLinkStorage.Set(paymentLinkDto);
    }
}