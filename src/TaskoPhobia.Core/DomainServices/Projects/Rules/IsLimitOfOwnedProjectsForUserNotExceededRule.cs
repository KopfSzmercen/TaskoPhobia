using TaskoPhobia.Core.Entities.Users;
using TaskoPhobia.Core.ValueObjects;
using TaskoPhobia.Shared.Abstractions.Domain;

namespace TaskoPhobia.Core.DomainServices.Projects.Rules;

internal sealed class IsLimitOfOwnedProjectsForUserNotExceededRule : IBusinessRule
{
    private const ushort MaxNumberOfCreatedProjectsForFreeAccount = 3;
    private const ushort MaxNumberOfCreatedProjectsForBasicAccount = 6;
    private readonly int _numberOfProjectOwnedByUser;
    private readonly User _user;

    public IsLimitOfOwnedProjectsForUserNotExceededRule(User user, int numberOfProjectOwnedByUser)
    {
        _user = user;
        _numberOfProjectOwnedByUser = numberOfProjectOwnedByUser;
    }

    public string Message =>
        $"Can't create project. Max number of projects for this account type is " +
        $"{(_user.AccountType.Equals(AccountType.Free()) ? MaxNumberOfCreatedProjectsForFreeAccount : MaxNumberOfCreatedProjectsForBasicAccount)}";

    public bool IsBroken()
    {
        if (_user.AccountType == AccountType.Extended()) return false;

        if (_user.AccountType == AccountType.Free() &&
            _numberOfProjectOwnedByUser >= MaxNumberOfCreatedProjectsForFreeAccount) return true;

        return _numberOfProjectOwnedByUser >= MaxNumberOfCreatedProjectsForBasicAccount;
    }
}