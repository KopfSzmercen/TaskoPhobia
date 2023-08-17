using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskoPhobia.Application.Commands.AccountUpgradeProducts.SeedAccountUpgradeProducts;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Exceptions.Errors;
using TaskoPhobia.Shared.AuthorizationPolicies;

namespace TaskoPhobia.Api.Controllers.AccountUpgradeProducts;

[Route("account-upgrade-products")]
public class AccountUpgradeProductsController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AccountUpgradeProductsController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Authorize(nameof(AuthorizationPolicies.AdminPolicy))]
    [HttpPost]
    [SwaggerOperation("Seed account upgrade products")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Get()
    {
        var command = new SeedAccountUpgradeProducts();
        await _commandDispatcher.DispatchAsync(command);

        return Ok();
    }
}