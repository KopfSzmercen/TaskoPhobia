using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.Payments.Events;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Shared.Events;

namespace TaskoPhobia.Application.EventHandlers;

internal sealed class UpgradeAccountWhenOrderIsPaidDomainEventHandler : IDomainEventHandler<PaymentCompletedDomainEvent>
{
    private readonly IAccountUpgradeProductRepository _accountUpgradeProductRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;

    public UpgradeAccountWhenOrderIsPaidDomainEventHandler(
        IAccountUpgradeProductRepository accountUpgradeProductRepository, IOrderRepository orderRepository,
        IUserRepository userRepository)
    {
        _accountUpgradeProductRepository = accountUpgradeProductRepository;
        _orderRepository = orderRepository;
        _userRepository = userRepository;
    }

    public async Task HandleAsync(PaymentCompletedDomainEvent domainEvent)
    {
        var order = await _orderRepository.FindByIdAsync(domainEvent.OrderId);

        if (order is null) throw new OrderNotFound();

        order.Complete();

        await _orderRepository.UpdateAsync(order);

        var accountUpgradeProduct = await _accountUpgradeProductRepository.FindByIdAsync(order.ProductId);

        if (accountUpgradeProduct is null) return;

        var user = await _userRepository.FindByIdAsync(order.CustomerId);

        if (user is null) throw new UserNotFoundException(order.CustomerId);

        user.SetAccountType(accountUpgradeProduct.UpgradeTypeValue);

        await _userRepository.UpdateAsync(user);
    }
}