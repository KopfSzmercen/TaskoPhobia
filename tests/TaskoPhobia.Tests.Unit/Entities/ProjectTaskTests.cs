using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.ProjectTasks;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Time;
using TaskoPhobia.Shared.Time;

namespace TaskoPhobia.Tests.Unit.Entities;

public class ProjectTaskTests : TestBase
{
    #region Setup

    private static ProjectTask CreateProjectTask()
    {
        return ProjectTask.CreateNew(Guid.NewGuid(), "Name", new TaskTimeSpan(Clock.Now(), Clock.Now().AddDays(3)),
            TaskAssignmentLimit, CreateProject());
    }


    private static Project CreateProject()
    {
        return Project.CreateNew(Guid.NewGuid(), "Name", "description", Clock, Guid.NewGuid());
    }

    private static readonly IClock Clock = new Clock();
    private static readonly int TaskAssignmentLimit = 2;

    #endregion
}