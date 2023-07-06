using Shouldly;
using TaskoPhobia.Core.Entities;
using TaskoPhobia.Core.Exceptions;
using TaskoPhobia.Core.ValueObjects;
using Xunit;

namespace TaskoPhobia.Tests.Unit.Entities;

public class UserTests
{
    [Fact]
    public void
        add_project_when_basic_account_exceeded_max_number_of_project_throws_NotAllowedToCreateMoreProjectsException()
    {
        var user = CreateUserWithGivenAccountType(AccountType.Basic());

        for (var i = 0; i < User.MaxNumOfProjectsForBasicAccount; i++) user.AddProject(CreateProjectForUser(user.Id));
        Assert.Throws<NotAllowedToCreateMoreProjectsException>(() => user.AddProject(CreateProjectForUser(user.Id)));
    }

    [Fact]
    public void
        add_project_when_free_account_exceeded_max_number_of_project_throws_NotAllowedToCreateMoreProjectsException()
    {
        var user = CreateUserWithGivenAccountType(AccountType.Free());

        for (var i = 0; i < User.MaxNumOfProjectsForFreeAccount; i++) user.AddProject(CreateProjectForUser(user.Id));
        Assert.Throws<NotAllowedToCreateMoreProjectsException>(() => user.AddProject(CreateProjectForUser(user.Id)));
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
        var project = new Project(Guid.NewGuid(), "Project", "description", ProgressStatus.Finished(), DateTime.Now,
            ownerId);
        return project;
    }

    #endregion
}