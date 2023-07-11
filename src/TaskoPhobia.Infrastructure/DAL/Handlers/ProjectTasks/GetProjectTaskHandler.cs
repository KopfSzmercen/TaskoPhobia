using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.ProjectTasks;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.ProjectTasks;

internal sealed class GetProjectTaskHandler : IQueryHandler<GetProjectTask, ProjectTaskDto>
{
    private readonly DbSet<ProjectTaskReadModel> _projectTasks;

    public GetProjectTaskHandler(TaskoPhobiaReadDbContext dbContext)
    {
        _projectTasks = dbContext.ProjectTasks;
    }

    public async Task<ProjectTaskDto> HandleAsync(GetProjectTask query)
    {
        
        var projectTask = await _projectTasks
            .Where(x => x.Id == query.ProjectTaskId && x.Project.Id == query.ProjectId && x.Project.OwnerId == query.UserId)
            .AsNoTracking()
            .SingleOrDefaultAsync();

        return projectTask?.AsDto();
    }
}