using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers;

internal sealed class GetProjectTaskHandler : IQueryHandler<GetProjectTask ,ProjectTaskDto>
{
    private readonly DbSet<ProjectTask> _projectTasks;

    public GetProjectTaskHandler(TaskoPhobiaDbContext dbContext)
    {
        _projectTasks = dbContext.ProjectTasks;
    }

    public async Task<ProjectTaskDto> HandleAsync(GetProjectTask query)
    {
        var projectTaskId = new ProjectTaskId(query.ProjectTaskId);
        var userId = new UserId(query.UserId);
        var projectId = new ProjectId(query.ProjectId);
        
        var projectTask = await _projectTasks
            .Where(x => x.Id == projectTaskId && x.Project.Id == projectId && x.Project.OwnerId == userId)
            .AsNoTracking()
            .SingleOrDefaultAsync();

        return projectTask?.AsDto();
    }
}