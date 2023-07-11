using Microsoft.Extensions.DependencyInjection;

namespace TaskoPhobia.Core.DomainServices;

public static class Extensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<IInvitationService, InvitationService>();
        return services;
    }
}