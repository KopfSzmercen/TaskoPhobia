using TaskoPhobia.Core.DomainServices.Projects.Rules;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.Services;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;
using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Core.DomainServices.Projects;

internal sealed class ProjectService : DomainService, IProjectService
{
    private readonly IClock _clock;
    private readonly IProjectReadService _projectReadService;

    public ProjectService(IProjectReadService projectReadService, IClock clock)
    {
        _projectReadService = projectReadService;
        _clock = clock;
    }

    public async Task<Project> CreateProject(Guid projectId, ProjectName projectName,
        ProjectDescription projectDescription, User owner)
    {
        var numberOfProjectOwnedByUser = await _projectReadService.CountOwnedByUserAsync(owner.Id);

        CheckRule(new IsLimitOfOwnedProjectsForUserNotExceededRule(owner, numberOfProjectOwnedByUser));

        return Project.CreateNew(projectId, projectName, projectDescription, _clock, owner.Id);
    }
}