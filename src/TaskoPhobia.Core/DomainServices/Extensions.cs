using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Core.DomainServices.Invitations;
using TaskoPhobia.Core.DomainServices.Projects;

namespace TaskoPhobia.Core.DomainServices;

public static class Extensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IInvitationService, InvitationService>();
        services.AddScoped<IProjectService, ProjectService>();
        return services;
    }
}