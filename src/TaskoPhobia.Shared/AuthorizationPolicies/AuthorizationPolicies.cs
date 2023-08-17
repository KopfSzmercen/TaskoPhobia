namespace TaskoPhobia.Shared.AuthorizationPolicies;

public static class AuthorizationPolicies
{
    public static string AdminPolicy { get; } = "RequireAdminPolicy";
    public static string UserPolicy { get; } = "RequireUserPolicy";
}