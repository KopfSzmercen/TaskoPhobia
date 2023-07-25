namespace TaskoPhobia.Shared.Abstractions.Contexts;

public interface IContext
{
    Guid RequestId { get; }
    IIdentityContext Identity { get; }
}