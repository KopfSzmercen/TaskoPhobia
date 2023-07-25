using System.Security.Claims;
using TaskoPhobia.Shared.Abstractions.Contexts;

namespace TaskoPhobia.Shared.Contexts;

public class IdentityContext : IIdentityContext
{
    private IdentityContext()
    {
    }

    public IdentityContext(ClaimsPrincipal principal)
    {
        if (principal?.Identity is null || string.IsNullOrWhiteSpace(principal.Identity.Name)) return;

        IsAuthenticated = principal.Identity?.IsAuthenticated is true;
        Id = IsAuthenticated ? Guid.Parse(principal.Identity.Name) : Guid.Empty;
        Role = principal?.Claims?.SingleOrDefault(x => x?.Type == ClaimTypes.Role)?.Value;
    }

    public static IIdentityContext Empty => new IdentityContext();
    public bool IsAuthenticated { get; }
    public Guid Id { get; }
    public string Role { get; }
}