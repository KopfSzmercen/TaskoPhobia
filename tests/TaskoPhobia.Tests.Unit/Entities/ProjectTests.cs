using Shouldly;
using TaskoPhobia.Core.DomainServices.Invitations.Rules;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Time;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Unit.Entities;

public class ProjectTests : TestBase
{
    [Fact]
    public void add_task_when_project_is_finished_should_throw_NotAllowedToModifyFinishedProject_exception()
    {
        var project = CreateProjectWithGivenStatus(ProgressStatus.Finished());
        AssertBrokenRule<FinishedProjectCanNotBeModifiedRule>(() => project.AddTask(CreateTaskForProject(project.Id)));
    }

    [Fact]
    public void add_task_when_project_is_in_progress_should_work()
    {
        var project = CreateProjectWithGivenStatus(ProgressStatus.InProgress());
        project.AddTask(CreateTaskForProject(project.Id));
        project.Tasks.Count().ShouldBe(1);
    }


    #region setup

    private static readonly IClock Clock = new Clock();

    private static Project CreateProjectWithGivenStatus(ProgressStatus progressStatus)
    {
        var project = Project.CreateNew(Guid.NewGuid(), "Project", "description", new Clock(),
            Guid.NewGuid());

        if (progressStatus == ProgressStatus.Finished()) project.SetStatusToFinished();
        return project;
    }

    private static ProjectTask CreateTaskForProject(ProjectId projectId)
    {
        var task = ProjectTask.CreateNew(Guid.NewGuid(), "Project",
            new TaskTimeSpan(DateTime.UtcNow, DateTime.UtcNow.AddDays(5)), projectId);

        return task;
    }

    #endregion
}