namespace TaskoPhobia.Shared.Abstractions.Domain;

public interface IBusinessRuleAsync
{
    string Message { get; }
    Task<bool> IsBroken();
}