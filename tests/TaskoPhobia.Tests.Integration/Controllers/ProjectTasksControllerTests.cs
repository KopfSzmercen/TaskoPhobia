using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using TaskoPhobia.Application.Commands.ProjectTasks.CreateProjectTask;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Infrastructure.Security;
using TaskoPhobia.Shared.Abstractions.Time;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Integration.Controllers;

public class ProjectTasksControllerTests : ControllerTests, IDisposable
{
    [Fact]
    public async Task given_valid_task_data_post_tasks_should_return_status_201_created()
    {
        var project = await CreateUserWithProjectAndAuthorizeAsync();
        var request = new CreateProjectTaskRequest
        {
            End = _clock.Now().AddDays(5),
            Start = _clock.Now(),
            TaskName = "Task name"
        };

        var response = await HttpClient.PostAsJsonAsync($"/projects/{project.Id.Value}/tasks", request);
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ShouldNotBeNull();
    }

    [Fact]
    public async Task given_existing_task_id_get_task_should_return_status_200_ok()
    {
        var project = await CreateUserWithProjectAndAuthorizeAsync();
        var task = await CreateTaskForProject(project.Id);

        var response = await HttpClient.GetAsync($"projects/{project.Id.Value}/tasks/{task.Id.Value}");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var taskDto = await response.Content.ReadFromJsonAsync<ProjectTaskDto>();
        taskDto.ShouldNotBeNull();
        taskDto.Id.ShouldBe(task.Id.Value);
    }

    [Fact]
    public async Task having_project_with_tasks_get_task_should_return_tasks_and_status_200_ok()
    {
        var project = await CreateUserWithProjectAndAuthorizeAsync();
        await CreateTaskForProject(project.Id);

        var response = await HttpClient.GetAsync($"projects/{project.Id.Value}/tasks");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var taskDtos = await response.Content.ReadFromJsonAsync<IEnumerable<ProjectTaskDto>>();
        taskDtos.Count().ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task given_non_existing_task_id_get_task_should_return_status_404_not_found()
    {
        var project = await CreateUserWithProjectAndAuthorizeAsync();

        var response = await HttpClient.GetAsync($"projects/{project.Id.Value}/tasks/{Guid.NewGuid().ToString()}");
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    #region setup

    private readonly TestDatabase _testDatabase;
    private readonly IClock _clock;

    public ProjectTasksControllerTests(OptionsProvider optionsProvider) : base(optionsProvider,
        new Clock())
    {
        _testDatabase = new TestDatabase();
        _clock = new Clock();
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }

    private async Task<Project> CreateUserWithProjectAndAuthorizeAsync()
    {
        var passwordManager = new PasswordManager(new PasswordHasher<object>());
        var securedPassword = passwordManager.Secure("secret");

        var user = new User(Guid.NewGuid(), "testUser@t.pl",
            "testUser", securedPassword, Role.User(), DateTime.UtcNow, AccountType.Free());

        await _testDatabase.WriteDbContext.Users.AddAsync(user);
        await _testDatabase.WriteDbContext.SaveChangesAsync();

        Authorize(user.Id, user.Role);

        var project = new Project(Guid.NewGuid(), "Project name", "Project description", ProgressStatus.InProgress(),
            _clock.Now(), user.Id);

        await _testDatabase.WriteDbContext.Projects.AddAsync(project);
        await _testDatabase.WriteDbContext.SaveChangesAsync();

        return project;
    }

    private async Task<ProjectTask> CreateTaskForProject(ProjectId projectId)
    {
        var task = new ProjectTask(Guid.NewGuid(), "Task", new TaskTimeSpan(_clock.Now(), _clock.Now().AddDays(5)),
            projectId, ProgressStatus.InProgress());

        await _testDatabase.WriteDbContext.ProjectTasks.AddAsync(task);
        await _testDatabase.WriteDbContext.SaveChangesAsync();
        return task;
    }

    #endregion
}