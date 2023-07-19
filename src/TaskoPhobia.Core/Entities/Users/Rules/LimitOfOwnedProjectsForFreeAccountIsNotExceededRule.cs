using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Users.Rules;

public class LimitOfOwnedProjectsForFreeAccountIsNotExceededRule : IBusinessRule
{
    public const ushort MaxNumberOfOwnedProjectsForFreeAccount = 3;
    private readonly User _user;

    public LimitOfOwnedProjectsForFreeAccountIsNotExceededRule(User user)
    {
        _user = user;
    }

    public string Message =>
        $"You can't create more projects. Max number of owned project for free account is {MaxNumberOfOwnedProjectsForFreeAccount}";

    public bool IsBroken()
    {
        return _user.AccountType.Equals(AccountType.Free()) &&
               _user.OwnedProjects.Count + 1 > MaxNumberOfOwnedProjectsForFreeAccount;
    }
}