using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Entities.Payments.Events;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Shared.Events;

namespace TaskoPhobia.Application.EventHandlers;

internal sealed class CompleteOrderWhenPaymentIsCompletedEventHandler : IDomainEventHandler<PaymentCompletedDomainEvent>
{
    private readonly IOrderRepository _orderRepository;

    public CompleteOrderWhenPaymentIsCompletedEventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task HandleAsync(PaymentCompletedDomainEvent domainEvent)
    {
        var order = await _orderRepository.FindByIdAsync(domainEvent.OrderId);

        if (order is null) throw new OrderNotFound();

        order.Complete();

        await _orderRepository.UpdateAsync(order);
    }
}