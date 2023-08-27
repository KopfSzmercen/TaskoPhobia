using TaskoPhobia.Application.Commands.AccountUpgradeProducts.OrderAccountUpgradeProduct.Exceptions;
using TaskoPhobia.Application.Exceptions;
using TaskoPhobia.Core.DomainServices.Orders;
using TaskoPhobia.Core.Entities.AccountUpgradeProducts;
using TaskoPhobia.Core.Entities.Products;
using TaskoPhobia.Core.Repositories;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Contexts;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Application.Commands.AccountUpgradeProducts.OrderAccountUpgradeProduct;

internal sealed class OrderAccountUpgradeProductHandler : ICommandHandler<OrderAccountUpgradeProduct>
{
    private readonly IAccountUpgradeProductRepository _accountUpgradeProductRepository;
    private readonly IClock _clock;
    private readonly IContext _context;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrdersService _ordersService;
    private readonly IUserRepository _userRepository;

    public OrderAccountUpgradeProductHandler(IAccountUpgradeProductRepository accountUpgradeProductRepository,
        IContext context, IUserRepository userRepository, IOrdersService ordersService, IClock clock,
        IOrderRepository orderRepository)
    {
        _accountUpgradeProductRepository = accountUpgradeProductRepository;
        _context = context;
        _userRepository = userRepository;
        _ordersService = ordersService;
        _clock = clock;
        _orderRepository = orderRepository;
    }

    public async Task HandleAsync(OrderAccountUpgradeProduct command)
    {
        var accountUpgradeProduct = await _accountUpgradeProductRepository.FindByIdAsync(command.ProductId);
        if (accountUpgradeProduct is null) throw new AccountUpgradeProductNotFound();

        var user = await _userRepository.FindByIdAsync(_context.Identity.Id);
        if (user is null) throw new UserNotFoundException(_context.Identity.Id);

        var order = _ordersService.CreateOrderForAccountUpgradeProduct(command.OrderId, accountUpgradeProduct, user,
            _clock.DateTimeOffsetNow());

        await _orderRepository.AddAsync(order);
    }
}