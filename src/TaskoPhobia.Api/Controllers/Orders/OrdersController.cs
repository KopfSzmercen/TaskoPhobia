using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskoPhobia.Application.Commands.Payments.CreatePaymentLink;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.Orders;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Payments;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Api.Controllers.Orders;

[Authorize]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IPaymentLinkStorage _paymentLinkStorage;
    private readonly IQueryDispatcher _queryDispatcher;

    public OrdersController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher,
        IPaymentLinkStorage paymentLinkStorage)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
        _paymentLinkStorage = paymentLinkStorage;
    }

    [HttpGet]
    [SwaggerOperation("Browse orders")]
    [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<OrderDto>> Get()
    {
        return await _queryDispatcher.QueryAsync(new BrowseOrders());
    }

    [HttpPost("{orderId:guid}/payments")]
    [SwaggerOperation("Create payment link")]
    [ProducesResponseType(typeof(PaymentLinkDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaymentLinkDto>> Post([FromRoute] Guid orderId)
    {
        var command = new CreatePaymentLink(orderId);
        await _commandDispatcher.DispatchAsync(command);

        var paymentLink = _paymentLinkStorage.Get();

        return Ok(paymentLink);
    }
}