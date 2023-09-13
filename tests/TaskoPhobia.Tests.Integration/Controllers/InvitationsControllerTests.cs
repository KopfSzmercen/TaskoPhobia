using System.Net;
using System.Net.Http.Json;
using Shouldly;
using TaskoPhobia.Application.Commands.Invitations.CreateInvitation;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Core.Entities.Invitations;
using TaskoPhobia.Shared.Abstractions.Queries;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Integration.Controllers;

public class InvitationsControllerTests : ControllerTests
{
    public InvitationsControllerTests(OptionsProvider optionsProvider) : base(optionsProvider,
        new Clock())
    {
    }


    [Fact]
    public async Task GivenUserIsOwnerOfProject_PostInvitation_ShouldCreateInvitation()
    {
        var receiver = await CreateUserAsync();
        var (project, sender) = await CreateUserWithProjectAsync();

        var request = new CreateInvitationRequest
        {
            ReceiverId = receiver.Id
        };

        Authorize(sender.Id, sender.Role);

        var response = await HttpClient.PostAsJsonAsync($"/projects/{project.Id.Value}/invitations", request);
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    public async Task GivenValidCredentials_GetInvitations_ShouldReturnSentInvitationsToProject()
    {
        var (project, sender) = await CreateUserWithProjectAsync();

        var receiver = await CreateUserAsync();
        var invitation = Invitation.CreateNew(Guid.NewGuid(), project.Id, "title", sender.Id, receiver.Id, new Clock());

        await _testDatabase.WriteDbContext.Invitations.AddAsync(invitation);

        await _testDatabase.WriteDbContext.SaveChangesAsync();

        Authorize(sender.Id, receiver.Role);

        var response = await HttpClient.GetAsync($"/projects/{project.Id.Value}/invitations");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var results = await response.Content.ReadFromJsonAsync<Paged<SentInvitationDto>>();

        results.Items.ShouldNotBeNull();
        results.Items.Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task GivenValidCredentials_GetInvitationShouldReturnInvitation()
    {
        var (project, sender) = await CreateUserWithProjectAsync();

        var receiver = await CreateUserAsync();
        var invitation = Invitation.CreateNew(Guid.NewGuid(), project.Id, "title", sender.Id, receiver.Id, new Clock());

        await _testDatabase.WriteDbContext.Invitations.AddAsync(invitation);

        await _testDatabase.WriteDbContext.SaveChangesAsync();

        Authorize(sender.Id, sender.Role);

        var response = await HttpClient.GetAsync($"/projects/{project.Id.Value}/invitations/{invitation.Id.Value}");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<SentInvitationDto>();

        result.ShouldNotBeNull();
        result.Id.ShouldBe(invitation.Id.Value);
    }

    [Fact]
    public async Task GivenNonExistingInvitationId_GetInvitationShouldReturn404StatusCode()
    {
        var (project, sender) = await CreateUserWithProjectAsync();

        Authorize(sender.Id, sender.Role);

        var response =
            await HttpClient.GetAsync($"/projects/{project.Id.Value}/invitations/{Guid.NewGuid().ToString()}");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}