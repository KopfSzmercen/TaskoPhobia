using Microsoft.AspNetCore.Mvc;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Api.Controllers.Invitations.Requests;

internal sealed class BrowseSentInvitationsRequest
{
    [FromRoute(Name = "projectId")] public Guid ProjectId { get; init; }
    [FromQuery] public string Search { get; init; }
    [FromQuery] public int Page { get; init; }
    [FromQuery] public int Results { get; init; }
    [FromQuery] public string OrderBy { get; init; }
    [FromQuery] public IPagedQuery.SortOrderOptions SortOrder { get; init; }
}