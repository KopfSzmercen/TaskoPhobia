using Microsoft.AspNetCore.Mvc;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Api.Controllers.ProjectTasks.Requests;

public class BrowseProjectTasksRequest : IPagedQuery
{
    [FromRoute(Name = "projectId")] public Guid ProjectId { get; init; }
    [FromQuery] public int Page { get; set; }
    [FromQuery] public int Results { get; set; }
    [FromQuery] public string OrderBy { get; set; }
    [FromQuery] public IPagedQuery.SortOrderOptions SortOrder { get; set; }
}