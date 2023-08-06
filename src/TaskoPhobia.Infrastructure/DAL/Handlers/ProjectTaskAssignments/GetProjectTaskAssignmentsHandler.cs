using Microsoft.EntityFrameworkCore;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Queries.ProjectTaskAssignments;
using TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;
using TaskoPhobia.Infrastructure.DAL.Contexts;
using TaskoPhobia.Shared.Abstractions.Contexts;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Infrastructure.DAL.Handlers.ProjectTaskAssignments;

internal sealed class
    GetProjectTaskAssignmentsHandler : IQueryHandler<GetProjectTaskAssignment, ProjectTaskAssignmentDto>
{
    private readonly IContext _context;
    private readonly DbSet<ProjectTaskAssignmentReadModel> _projectTaskAssignments;

    public GetProjectTaskAssignmentsHandler(IContext context, TaskoPhobiaReadDbContext readContext)
    {
        _context = context;
        _projectTaskAssignments = readContext.ProjectTaskAssignments;
    }

    public async Task<ProjectTaskAssignmentDto> HandleAsync(GetProjectTaskAssignment query)
    {
        var projectTaskAssignments = _projectTaskAssignments.AsNoTracking()
            .Where(x => x.Id == query.ProjectTaskAssignmentId);

        projectTaskAssignments = AssignmentsWhereUserOwnsProjectOrIsAssignedToTask(projectTaskAssignments);

        projectTaskAssignments = AssignmentsWhichBelongToGivenTaskAndProject(projectTaskAssignments, query);

        return await projectTaskAssignments.Select(x => x.AsDto())
            .FirstOrDefaultAsync();
    }

    private IQueryable<ProjectTaskAssignmentReadModel> AssignmentsWhereUserOwnsProjectOrIsAssignedToTask(
        IQueryable<ProjectTaskAssignmentReadModel> projectTaskAssignments)
    {
        return projectTaskAssignments.Where(x =>
            x.ProjectTask.Project.OwnerId == _context.Identity.Id ||
            x.ProjectTask.Assignments.Any(assignment => assignment.AssigneeId == _context.Identity.Id));
    }

    private IQueryable<ProjectTaskAssignmentReadModel> AssignmentsWhichBelongToGivenTaskAndProject(
        IQueryable<ProjectTaskAssignmentReadModel> projectTaskAssignments, GetProjectTaskAssignment query)
    {
        return projectTaskAssignments.Where(x =>
            x.ProjectTask.Id == query.ProjectTaskId && x.ProjectTask.ProjectId == query.ProjectId);
    }
}