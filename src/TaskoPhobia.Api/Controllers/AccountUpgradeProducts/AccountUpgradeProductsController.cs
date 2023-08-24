using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskoPhobia.Application.Commands.AccountUpgradeProducts.OrderAccountUpgradeProduct;
using TaskoPhobia.Application.Commands.AccountUpgradeProducts.SeedAccountUpgradeProducts;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.AccountUpgradeProducts;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Exceptions.Errors;
using TaskoPhobia.Shared.Abstractions.Queries;
using TaskoPhobia.Shared.AuthorizationPolicies;

namespace TaskoPhobia.Api.Controllers.AccountUpgradeProducts;

[Route("account-upgrade-products")]
public class AccountUpgradeProductsController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public AccountUpgradeProductsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [Authorize(nameof(AuthorizationPolicies.AdminPolicy))]
    [HttpPost]
    [SwaggerOperation("Seed account upgrade products")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post()
    {
        var command = new SeedAccountUpgradeProducts();
        await _commandDispatcher.DispatchAsync(command);

        return Ok();
    }

    [HttpGet]
    [SwaggerOperation("Browse available account upgrade products")]
    [ProducesResponseType(typeof(IEnumerable<AccountUpgradeProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AccountUpgradeProductDto>>> Get()
    {
        var result = await _queryDispatcher.QueryAsync(new BrowseAccountUpgradeProducts());
        return Ok(result);
    }

    [Authorize]
    [HttpPost("{productId:guid}/orders")]
    [SwaggerOperation("Create order for a product")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateOrder([FromRoute] Guid productId)
    {
        var orderId = Guid.NewGuid();
        var command = new OrderAccountUpgradeProduct(orderId, productId);

        await _commandDispatcher.DispatchAsync(command);

        return CreatedAtAction(nameof(Get), new { command.OrderId }, null);
    }
}