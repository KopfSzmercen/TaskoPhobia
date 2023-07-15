using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Core.DomainServices;
using TaskoPhobia.Core.Policies;

namespace TaskoPhobia.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddDomainServices();
        services.AddSingleton<ICreateInvitationPolicy, ProjectOwnerPolicy>();
        return services;
    }
}