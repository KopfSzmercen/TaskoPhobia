using Microsoft.AspNetCore.Http;
using TaskoPhobia.Shared.Abstractions.Contexts;

namespace TaskoPhobia.Shared.Contexts;

public class Context : IContext
{
    private Context(IIdentityContext identity = null)
    {
        Identity = identity ?? IdentityContext.Empty;
    }

    public Context(HttpContext context) : this(new IdentityContext(context.User))
    {
    }

    public Guid RequestId { get; } = Guid.NewGuid();
    public IIdentityContext Identity { get; }
}