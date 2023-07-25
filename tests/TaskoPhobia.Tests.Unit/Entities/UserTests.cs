using Shouldly;
using TaskoPhobia.Core.Entities.Projects;
using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.Entities.Users.Rules;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Time;
using Xunit;

namespace TaskoPhobia.Tests.Unit.Entities;

public class UserTests : TestBase
{
    [Fact]
    public void
        add_project_when_basic_account_exceeded_max_number_of_project_throws_NotAllowedToCreateMoreProjectsException()
    {
        var user = CreateUserWithGivenAccountType(AccountType.Basic());
        const ushort limitOfProjectsForBasicAccount = LimitOfOwnedProjectsForBasicAccountIsNotExceededRule
            .MaxNumberOfOwnedProjectsForBasicAccount;

        for (var i = 0; i < limitOfProjectsForBasicAccount; i++) user.AddProject(CreateProjectForUser(user.Id));

        AssertBrokenRule<LimitOfOwnedProjectsForBasicAccountIsNotExceededRule>(() =>
            user.AddProject(CreateProjectForUser(user.Id)));
    }

    [Fact]
    public void
        add_project_when_free_account_exceeded_max_number_of_project_throws_NotAllowedToCreateMoreProjectsException()
    {
        var user = CreateUserWithGivenAccountType(AccountType.Free());
        const ushort limitOfProjectsForFreeAccount =
            LimitOfOwnedProjectsForFreeAccountIsNotExceededRule.MaxNumberOfOwnedProjectsForFreeAccount;

        for (var i = 0; i < limitOfProjectsForFreeAccount; i++) user.AddProject(CreateProjectForUser(user.Id));

        AssertBrokenRule<LimitOfOwnedProjectsForFreeAccountIsNotExceededRule>(() =>
            user.AddProject(CreateProjectForUser(user.Id)));
    }

    [Fact]
    public void
        add_project_for_user_should_work()
    {
        var user = CreateUserWithGivenAccountType(AccountType.Basic());
        user.AddProject(CreateProjectForUser(user.Id));

        user.OwnedProjects.Count.ShouldBe(1);
    }

    #region setup

    private static User CreateUserWithGivenAccountType(AccountType accountType)
    {
        var user = new User(Guid.NewGuid(),
            "email@e.pl",
            "username",
            "secret", Role.User(),
            DateTime.Now.Date, accountType);
        return user;
    }


    private static Project CreateProjectForUser(UserId ownerId)
    {
        var project = Project.CreateNew(Guid.NewGuid(), "Project", "description", new Clock(),
            ownerId);
        return project;
    }

    #endregion
}