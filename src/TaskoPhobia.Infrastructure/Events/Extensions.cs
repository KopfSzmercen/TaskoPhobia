using Microsoft.Extensions.DependencyInjection;
using TaskoPhobia.Shared.Events;

namespace TaskoPhobia.Infrastructure.Events;

internal static class Extensions
{
    public static IServiceCollection AddDomainEventsDispatching(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        services.AddScoped<IDomainEventsAccessor, DomainEventsAccessor>();

        return services;
    }
}