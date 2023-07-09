﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskoPhobia.Application.Commands.Invitations.CreateInvitation;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries;
using TaskoPhobia.Application.Queries.Invitations;
using TaskoPhobia.Shared.Abstractions.Commands;
using TaskoPhobia.Shared.Abstractions.Exceptions.Errors;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Api.Controllers;

[Route("projects/{projectId:guid}/invitations")]
[Authorize]
public class InvitationsController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public InvitationsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost]
    [SwaggerOperation("Create an invitation to project")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorsResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateInvitationRequest request, [FromRoute] Guid projectId)
    {
        var command = request.ToCommand(projectId, GetUserId());
        await _commandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(Get), new { projectId, invitationId = command.InvitationId }, null);
    }

    [HttpGet]
    [SwaggerOperation("Browse sent invitations to a project")]
    [ProducesResponseType(typeof(IEnumerable<SentInvitationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SentInvitationDto>>> Get([FromRoute] Guid projectId)
    {
        var query = new BrowseSentInvitations(projectId, GetUserId());
        var invitations = await _queryDispatcher.QueryAsync(query);

        return Ok(invitations);
    }

    [HttpGet("{invitationId:guid}")]
    [SwaggerOperation("Get invitation")]
    [ProducesResponseType(typeof(SentInvitationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get([FromRoute] Guid projectId, [FromRoute] Guid invitationId)
    {
        var query = new GetSentInvitation(GetUserId(), projectId, invitationId);
        var invitation = await _queryDispatcher.QueryAsync(query);

        if (invitation is null) return NotFound();
        return Ok(invitation);
    }
}