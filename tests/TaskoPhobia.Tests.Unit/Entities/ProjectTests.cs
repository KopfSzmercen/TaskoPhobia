using Shouldly;
using TaskoPhobia.Core.Common.Rules;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.Projects.Rules;
using TaskoPhobia.Shared.Abstractions.Time;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Unit.Entities;

public class ProjectTests : TestBase
{
    [Fact]
    public void Trying_To_Finish_Project_Not_By_Its_Owner_Should_Throw_ProjectCanBeFinishedByItsOwnerRule_Exception()
    {
        var project = CreateProject();
        AssertBrokenRule<ProjectCanBeFinishedByItsOwnerRule>(() => project.Finish(Guid.NewGuid(), true));

        Should.NotThrow(() => project.Finish(project.OwnerId, true));
    }

    [Fact]
    public void
        Trying_To_Finish_Project_Not_Having_All_Tasks_Finished_Should_Throw_ProjectCanBeFinishedIfAllTasksAreFinished_Exception()
    {
        var project = CreateProject();

        AssertBrokenRule<ProjectCanBeFinishedIfAllTasksAreFinished>(() => project.Finish(project.OwnerId, false));

        Should.NotThrow(() => project.Finish(project.OwnerId, true));
    }

    [Fact]
    public void Trying_To_Modify_Finished_Project_ShouldFail()
    {
        var project = CreateProject();
        project.Finish(project.OwnerId, true);

        AssertBrokenRule<FinishedProjectCanNotBeModifiedRule>(() => project.Finish(Guid.NewGuid(), true));
    }

    #region Setup

    private static readonly IClock Clock = new Clock();

    private Project CreateProject()
    {
        return Project.CreateNew(Guid.NewGuid(), "Name", "description", Clock, Guid.NewGuid());
    }

    #endregion
}