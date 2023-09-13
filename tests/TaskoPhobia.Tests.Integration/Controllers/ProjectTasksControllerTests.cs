using System.Net;
using System.Net.Http.Json;
using Shouldly;
using TaskoPhobia.Application.Commands.ProjectTasks.CreateProjectTask;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.ProjectTasks;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Exceptions.Errors;
using TaskoPhobia.Shared.Abstractions.Queries;
using TaskoPhobia.Shared.Abstractions.Time;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Integration.Controllers;

public class ProjectTasksControllerTests : ControllerTests
{
    [Fact]
    public async Task given_valid_task_data_post_tasks_should_return_status_201_created()
    {
        var (project, user) = await CreateUserWithProjectAsync();
        var request = new CreateProjectTaskRequest
        {
            End = _clock.Now().AddDays(5),
            Start = _clock.Now(),
            TaskName = "Task name",
            AssignmentsLimit = 3
        };

        Authorize(user.Id, user.Role);
        var response = await HttpClient.PostAsJsonAsync($"/projects/{project.Id.Value}/tasks", request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ShouldNotBeNull();
    }

    [Fact]
    public async Task given_existing_task_id_get_task_should_return_status_200_ok()
    {
        var (project, user) = await CreateUserWithProjectAsync();
        var task = await CreateTaskForProject(project);

        Authorize(user.Id, user.Role);
        var response = await HttpClient.GetAsync($"projects/{project.Id.Value}/tasks/{task.Id.Value}");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var taskDto = await response.Content.ReadFromJsonAsync<ProjectTaskDto>();
        taskDto.ShouldNotBeNull();
        taskDto.Id.ShouldBe(task.Id.Value);
    }

    [Fact]
    public async Task having_project_with_tasks_get_task_should_return_tasks_and_status_200_ok()
    {
        var (project, user) = await CreateUserWithProjectAsync();
        await CreateTaskForProject(project);

        Authorize(user.Id, user.Role);

        var response = await HttpClient.GetAsync($"projects/{project.Id.Value}/tasks");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var taskDtos = await response.Content.ReadFromJsonAsync<Paged<ProjectTaskDto>>();
        taskDtos.Items.Count().ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task having_finished_project_post_task_should_return_400_bad_request()
    {
        var (project, user) = await CreateUserWithProjectAsync();
        project.Finish(project.OwnerId, true);
        await _testDatabase.WriteDbContext.SaveChangesAsync();

        var request = new CreateProjectTaskRequest
        {
            End = _clock.Now().AddDays(5),
            Start = _clock.Now(),
            TaskName = "Task name"
        };

        Authorize(user.Id, user.Role);

        var response = await HttpClient.PostAsJsonAsync($"/projects/{project.Id.Value}/tasks", request);
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var errorResult = response.Content.ReadFromJsonAsync<Error>().Result;
        errorResult.ShouldBeOfType<Error>();
    }

    [Fact]
    public async Task given_non_existing_task_id_get_task_should_return_status_404_not_found()
    {
        var (project, user) = await CreateUserWithProjectAsync();

        Authorize(user.Id, user.Role);

        var response = await HttpClient.GetAsync($"projects/{project.Id.Value}/tasks/{Guid.NewGuid().ToString()}");
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    #region setup

    private readonly IClock _clock;

    public ProjectTasksControllerTests(OptionsProvider optionsProvider) : base(optionsProvider,
        new Clock())
    {
        _clock = new Clock();
    }


    private async Task<ProjectTask> CreateTaskForProject(Project project)
    {
        var task = ProjectTask.CreateNew(Guid.NewGuid(), "Task",
            new TaskTimeSpan(_clock.Now(), _clock.Now().AddDays(5)),
            3, project);

        await _testDatabase.WriteDbContext.ProjectTasks.AddAsync(task);
        await _testDatabase.WriteDbContext.SaveChangesAsync();
        return task;
    }

    #endregion
}