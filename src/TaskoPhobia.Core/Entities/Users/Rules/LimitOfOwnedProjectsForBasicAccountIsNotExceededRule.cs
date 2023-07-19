using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.Entities.Users.Rules;

public class LimitOfOwnedProjectsForBasicAccountIsNotExceededRule : IBusinessRule
{
    public const ushort MaxNumberOfOwnedProjectsForBasicAccount = 6;
    private readonly User _user;

    public LimitOfOwnedProjectsForBasicAccountIsNotExceededRule(User user)
    {
        _user = user;
    }

    public string Message =>
        $"You can't create more projects. Max number of owned project for basic account is {MaxNumberOfOwnedProjectsForBasicAccount}";

    public bool IsBroken()
    {
        return _user.AccountType.Equals(AccountType.Basic()) &&
               _user.OwnedProjects.Count + 1 > MaxNumberOfOwnedProjectsForBasicAccount;
    }
}