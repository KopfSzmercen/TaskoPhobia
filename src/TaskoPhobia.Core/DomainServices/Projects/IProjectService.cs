using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;

namespace TaskoPhobia.Core.DomainServices.Projects;

public interface IProjectService
{
    Task<Project> CreateProject(Guid projectId, ProjectName projectName, ProjectDescription projectDescription,
        User owner);
}