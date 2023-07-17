using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Core.DomainServices;
using TaskoPhobia.Core.Policies.Invitations;

namespace TaskoPhobia.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddCreateInvitationPolicies();
        services.AddDomainServices();

        return services;
    }
}