using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using TaskoPhobia.Application.Commands.Projects.CreateProject;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Infrastructure.Security;
using TaskoPhobia.Shared.Abstractions.Queries;
using TaskoPhobia.Shared.Abstractions.Time;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Integration.Controllers;

public class ProjectsControllerTests : ControllerTests, IDisposable
{
    [Fact]
    public async Task given_valid_credentials_and_project_data_post_should_return_201_created_code()
    {
        await CreateUserAndAuthorizeAsync();
        var request = new CreateProjectRequest
        {
            ProjectName = "Project1",
            ProjectDescription = "Project description"
        };

        var response = await HttpClient.PostAsJsonAsync("/projects", request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ShouldNotBeNull();
    }

    [Fact]
    public async Task given_user_has_a_project_get_projects_should_return_projects_and_200_ok_code()
    {
        var user = await CreateUserAndAuthorizeAsync();
        await CreateProjectForUserAsync(user.Id);

        var response = await HttpClient.GetAsync("/projects?Created=true");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var projectDtos = await response.Content.ReadFromJsonAsync<Paged<ProjectDto>>();
        projectDtos.Items.Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task given_non_existing_project_id_get_project_should_return_404_not_found_code()
    {
        await CreateUserAndAuthorizeAsync();
        var response = await HttpClient.GetAsync($"/projects/{Guid.NewGuid()}");
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task given_existing_project_id_get_project_should_return_200_ok_code()
    {
        var user = await CreateUserAndAuthorizeAsync();
        var project = await CreateProjectForUserAsync(user.Id);
        var response = await HttpClient.GetAsync($"/projects/{project.Id.Value}");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var projectDto = await response.Content.ReadFromJsonAsync<ProjectDto>();
        projectDto.ShouldNotBeNull();
        projectDto.Id.ShouldBe(project.Id.Value);
    }

    #region setup

    private readonly TestDatabase _testDatabase;
    private readonly IClock _clock;

    public ProjectsControllerTests(OptionsProvider optionsProvider) : base(optionsProvider, new Clock())
    {
        _testDatabase = new TestDatabase();
        _clock = new Clock();
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }

    private async Task<User> CreateUserAndAuthorizeAsync()
    {
        var passwordManager = new PasswordManager(new PasswordHasher<object>());
        var securedPassword = passwordManager.Secure("secret");

        var user = new User(Guid.NewGuid(), "testUser@t.pl",
            "testUser", securedPassword, Role.User(), DateTime.UtcNow, AccountType.Free());

        await _testDatabase.WriteDbContext.Users.AddAsync(user);
        await _testDatabase.WriteDbContext.SaveChangesAsync();

        Authorize(user.Id, user.Role);

        return user;
    }

    private async Task<Project> CreateProjectForUserAsync(UserId userId)
    {
        var project = Project.CreateNew(Guid.NewGuid(), "Project name", "Project description",
            _clock, userId);

        await _testDatabase.WriteDbContext.Projects.AddAsync(project);
        await _testDatabase.WriteDbContext.SaveChangesAsync();

        return project;
    }

    #endregion
}