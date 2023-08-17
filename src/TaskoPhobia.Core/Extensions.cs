using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Core.DomainServices;

namespace TaskoPhobia.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddDomainServices();
        return services;
    }
}