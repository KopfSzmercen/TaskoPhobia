using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskoPhobia.Api.Attributes;
using TaskoPhobia.Api.Controllers.Payments.Requests;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Api.Controllers.Payments;

[Route("payments")]
public class PaymentsController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public PaymentsController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{paymentId:guid}")]
    [SwaggerOperation("Mock payment gateway webhook")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post([FromMultiSource] HandlePaymentWebhookRequest request)
    {
        var command = request.ToCommand();
        await _commandDispatcher.DispatchAsync(command);

        return Ok();
    }
}